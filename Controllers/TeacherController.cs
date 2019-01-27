using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UoUWebApp.Context;
using UoUWebApp.Models;
using UoUWebApp.Manager;
using System.Data.SqlClient;

namespace UoUWebApp.Controllers
{
    public class TeacherController : Controller
    {
        private UoUDBContext db = new UoUDBContext();

        public async Task<ActionResult> Index()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            var teachers = db.Teachers.Include(t => t.DepartmentModel).Include(t => t.DesignationModel);
            return View(await teachers.ToListAsync());
        }

        public ActionResult Add()
        {
            ViewBag.TeacherDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.TeacherDesignationId = new SelectList(db.Designations, "DesignationId", "Designation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "TeacherId,TeacherName,TeacherAddress,TeacherEmail,TeacherContact,TeacherDesignationId,TeacherDeptId,TotalCredit")] TeacherModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacherModel);
                if(await db.SaveChangesAsync() > 0)
                {
                    TempData["Message"] = string.Format("Teacher added successfully!");
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("Index");
            }

            ViewBag.TeacherDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department", teacherModel.TeacherDeptId);
            ViewBag.TeacherDesignationId = new SelectList(db.Designations, "DesignationId", "Designation", teacherModel.TeacherDesignationId);
            return View(teacherModel);
        }

        [ActionName("assign-course")]
        public ActionResult AssignCourse()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.TeacherCourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.TeacherCourseTeacherId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };
            ViewBag.TeacherCourseCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };
            return View();
        }

        [ActionName("assign-course")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignCourse([Bind(Include = "TCId,TeacherCourseTeacherId,TeacherCourseCourseId,RecordStatus")] TeacherCourseModel teacherCourseModel)
        {
            db.TeacherCourses.Add(teacherCourseModel);
            if(await db.SaveChangesAsync() > 0)
            {
                var getCourse = db.Courses.Single(x => x.CourseId == teacherCourseModel.TeacherCourseCourseId);
                TempData["Message"] = string.Format(@"<b>{0}</b> assigned to <b>{1}</b> of <b>{2}</b> Department.", 
                                    getCourse.CourseName,
                                    db.Teachers.Single(x=>x.TeacherId == teacherCourseModel.TeacherCourseTeacherId).TeacherName,
                                    db.Departments.Single(x=>x.DeptId == getCourse.CourseDeptId).DeptName);
                TempData["MessageType"] = "success";
            }else
            {
                ViewBag.Message = "Course assigned failed!";
                ViewBag.MessageType = "danger";
            }
            ViewBag.TeacherCourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.TeacherCourseTeacherId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };
            ViewBag.TeacherCourseCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };

            return RedirectToAction("assign-course");
        }

        [ActionName("course-statics")]
        public ActionResult CourseStatics()
        {
            ViewBag.Departments = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            return View();
        }

        [HttpPost]
        public JsonResult GetByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(x => x.TeacherDeptId == deptId).ToList();
            var courses = db.Courses.Where(x => x.CourseDeptId == deptId).ToList();
            return Json(new {
                Teachers = teachers,
                Courses = courses
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTeacherCourses(int teacherId, int deptId)
        {
            var courses = new BusinessLogics().DepartmentActiveCourses(deptId);
            var CreditTaken = courses.AsEnumerable().Where(x => x.TeacherId == teacherId).Sum(y => y.CourseCredit);
            return Json(new {
                Courses = courses,
                CreditTaken = CreditTaken
            }
            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsEmailExists(string teacherEmail)
        {
            return Json(!db.Teachers.Any(x => x.TeacherEmail == teacherEmail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCourseStatics(int deptId)
        {
            return Json(new BusinessLogics().CourseStatics(deptId), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
