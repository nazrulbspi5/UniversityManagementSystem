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
using System.Globalization;

namespace UoUWebApp.Controllers
{
    public class StudentController : Controller
    {
        private UoUDBContext db = new UoUDBContext();

        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.DepartmentModel);
            return View(await students.ToListAsync());
        }

        public ActionResult Register()
        {
            ViewBag.StudentDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            if (TempData["student"] != null)
                return View(TempData["student"] as StudentModel);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "StudentId,StudentName,StudentEmail,StudentContact,RegDate,StudentAddress,StudentDeptId")] StudentModel studentModel)
        {
            string regPart = string.Format("{0}-{1}-", db.Departments.Single(x => x.DeptId == studentModel.StudentDeptId).DeptCode, studentModel.RegDate.Year.ToString());
            studentModel.StudentRegNo = new BusinessLogics().RegistrationNo(regPart);
            if (ModelState.IsValid)
            {
                db.Students.Add(studentModel);
                await db.SaveChangesAsync();
                TempData["student"] = studentModel;
                return RedirectToAction("Register");
            }

            ViewBag.StudentDeptId = new SelectList(db.Departments, "DeptId", "DeptCode", studentModel.StudentDeptId);
            return View(studentModel);
        }

        [ActionName("enroll-course")]
        public ActionResult EnrollCourse()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.StudentCourseStudentId = new SelectList(db.Students.OrderBy(x => x.StudentRegNo), "StudentId", "StudentRegNo");
            ViewBag.StudentCourseCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Student Registration No First" } };
            return View();
        }

        [HttpPost]
        [ActionName("enroll-course")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnrollCourse([Bind(Include = "StudentCourseStudentId, StudentCourseCourseId, EnrollDate, RecordStatus")] StudentCourseModel studentCourseModel)
        {
            studentCourseModel.RecordStatus = 1;
            if (ModelState.IsValid)
            {
                db.StudentCourses.Add(studentCourseModel);
                if (await db.SaveChangesAsync() > 0)
                {
                    var student = new StudentManager().GetStudentById(studentCourseModel.StudentCourseStudentId);
                    TempData["Message"] = string.Format("<b>{0}</b> of <b>{1}</b> enrolled to <b>{2}</b>",
                                                            student.StudentName,
                                                            student.DepartmentModel.Department,
                                                            db.Courses.Where(x => x.CourseId == studentCourseModel.StudentCourseCourseId).Single().Course);
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("enroll-course");
            }
            var deptId = new StudentManager().GetStudentById(studentCourseModel.StudentCourseStudentId).StudentDeptId;
            // db.Students.Where(y => y.StudentId == studentCourseModel.StudentCourseStudentId).Single().StudentDeptId;
            ViewBag.StudentCourseStudentId = new SelectList(new StudentManager().GetStudentsOrderByRegNo(), "StudentId", "StudentRegNo", studentCourseModel.StudentCourseStudentId);
            ViewBag.StudentCourseCourseId = new SelectList(new CourseManager().GetCoursesByDepartment(deptId), "CourseId", "Course", studentCourseModel.StudentCourseCourseId);
            // db.Courses.Where(x => x.CourseDeptId == deptId).OrderBy(z => z.CourseCode).ToList()
            return View(studentCourseModel);
        }

        [ActionName("add-result")]
        public ActionResult AddResult()
        {

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.StudentCourseStudentId = new SelectList(new StudentManager().GetStudentsOrderByRegNo(), "StudentId", "StudentRegNo");
            ViewBag.StudentCourseCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Student Registration No First" } };
            ViewBag.Grade = new SelectList(new StudentManager().GetGrades(), "Grade", "Grade");
            return View();
        }

        [HttpPost]
        [ActionName("add-result")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddResult([Bind(Include = "StudentCourseStudentId, StudentCourseCourseId, Grade")] StudentCourseModel studentCourseModel)
        {
            if (ModelState.IsValid)
            {
                var studentCourse = db.StudentCourses.Where(x => x.StudentCourseCourseId == studentCourseModel.StudentCourseCourseId && x.StudentCourseStudentId == studentCourseModel.StudentCourseStudentId && x.RecordStatus == 1).Single();
                studentCourse.Grade = studentCourseModel.Grade;
                db.Entry(studentCourse).State = EntityState.Modified;
                if(await db.SaveChangesAsync() > 0)
                {
                    TempData["Message"] = string.Format("<b>{0}</b> set for <b>{1}</b> enrolled by <b>{2}</b>", 
                                                            studentCourse.Grade,
                                                            new CourseManager().GetCourseById(studentCourse.StudentCourseCourseId).CourseName,
                                                            new StudentManager().GetStudentById(studentCourse.StudentCourseStudentId).StudentName);
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("add-result");
            }
            ViewBag.StudentCourseStudentId = new SelectList(new StudentManager().GetStudentsOrderByRegNo(), "StudentId", "StudentRegNo");
            ViewBag.StudentCourseCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Student Registration No First" } };
            ViewBag.Grade = new SelectList(new StudentManager().GetGrades(), "Grade", "Grade");
            return View(studentCourseModel);
        }

        [ActionName("view-result")]
        public ActionResult ViewResult()
        {
            ViewBag.StudentCourseStudentId = new SelectList(new StudentManager().GetStudentsOrderByRegNo(), "StudentId", "StudentRegNo");
            return View();
        }

        public ActionResult ResultView(int id)
        {
            var results = new StudentManager().GetResult(id);
            return View(results);
        }

        public ActionResult Print(int id)
        {
            var results = new StudentManager().GetResult(id);
            return new RazorPDF.PdfResult(results, "Print");
        }

        [HttpPost]
        public JsonResult GetStudentInfoById(int studentId)
        {
            var student = new StudentManager().GetStudentById(studentId);
            var courseIds = new CourseManager().GetCoursesByStudentId(studentId);
            var deptCourses = new CourseManager().GetCoursesByDepartment(student.StudentDeptId);
            return Json(
                new {
                    StudentName = student.StudentName,
                    StudentEmail = student.StudentEmail,
                    StudentDeptment = student.DepartmentModel.DeptName,
                    StudentCourses = courseIds,
                    DepartmentCourses = deptCourses
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStudentResult(int studentId)
        {
            var student = new StudentManager().GetStudentById(studentId);
            var courseResult = new StudentManager().GetResult(studentId);
            return Json(
                new
                {
                    StudentName = student.StudentName,
                    StudentEmail = student.StudentEmail,
                    StudentDeptment = student.DepartmentModel.DeptName,
                    CourseResult = courseResult
                }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult IsEmailExists(string studentEmail)
        {
            return Json(!db.Students.Any(x => x.StudentEmail == studentEmail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsEnrollDateOverlapRegDate(int studentCourseStudentId, DateTime enrollDate)
        {
            DateTime regDate = db.Students.Where(x => x.StudentId == studentCourseStudentId).Single().RegDate;
            var isValid = false;
            if(enrollDate >= regDate)
                isValid = true;
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsGradeEmpty(string grade)
        {
            var isValid_ = false;
            if (!string.IsNullOrEmpty(grade))
                isValid_ = true;
            return Json(isValid_, JsonRequestBehavior.AllowGet);
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
