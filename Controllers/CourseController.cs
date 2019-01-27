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

namespace UoUWebApp.Controllers
{
    public class CourseController : Controller
    {
        private UoUDBContext db = new UoUDBContext();

        public async Task<ActionResult> Index()
        {
            var courses = db.Courses.Include(c => c.DepartmentModel).Include(c => c.SemesterModel);
            return View(await courses.ToListAsync());
        }

        public ActionResult Create()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.CourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.CourseSemesterId = new SelectList(db.Semesters, "SemesterId", "Semester");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseId,CourseCode,CourseName,CourseCredit,CourseDesc,CourseDeptId,CourseSemesterId")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(courseModel);
                if(await db.SaveChangesAsync() > 0)
                {
                    TempData["Message"] = string.Format("<b>{0} ({1})</b> has been saved.", courseModel.CourseName, courseModel.CourseCode);
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("create");
            }

            ViewBag.CourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department", courseModel.CourseDeptId);
            ViewBag.CourseSemesterId = new SelectList(db.Semesters, "SemesterId", "Semester", courseModel.CourseSemesterId);
            return View(courseModel);
        }

        [ActionName("classroom-allocation")]
        public ActionResult ClassroomAllocation()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.CourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.RoomAllocationCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };
            ViewBag.RoomAllocationRoomId = new SelectList(db.Rooms.OrderBy(x => x.Room), "RoomId", "Room");
            ViewBag.RoomAllocationDayId = new SelectList(db.WeekDays.OrderBy(x => x.DayId), "DayId", "Day");
            return View();
        }

        [HttpPost]
        [ActionName("classroom-allocation")]
        public async Task<ActionResult> ClassroomAllocation([Bind(Include = "CourseDeptId, RoomAllocationCourseId, RoomAllocationRoomId, RoomAllocationDayId, FromTime, ToTime, RecordStatus")] RoomAllocationModel roomAllocationModel)
        {
            var fromTime = "000" + roomAllocationModel.FromTime.Replace(":", "");
            var toTime = "000" + roomAllocationModel.ToTime.Replace(":", "");
            roomAllocationModel.FromTime = fromTime.Substring(fromTime.Length - 4);
            roomAllocationModel.ToTime = toTime.Substring(toTime.Length - 4);

            if (ModelState.IsValid)
            {
                db.RoomAllocations.Add(roomAllocationModel);
                if(await db.SaveChangesAsync() > 0)
                {
                    TempData["Message"] = string.Format("Room <b>{0}</b> from <b>{1}</b> to <b>{2}</b> on <b>{3}</b> allocated to <b>{4}</b> course of <b>{5}</b> Department",
                                            db.Rooms.Where(r => r.RoomId == roomAllocationModel.RoomAllocationRoomId).Single().Room,
                                            new BusinessLogics().FromTime2Time(roomAllocationModel.FromTime),
                                            new BusinessLogics().ToTime2Time(roomAllocationModel.ToTime),
                                            db.WeekDays.Where(w => w.DayId == roomAllocationModel.RoomAllocationDayId).Single().Day,
                                            db.Courses.Where(c => c.CourseId == roomAllocationModel.RoomAllocationCourseId).Single().Course,
                                            db.Departments.Where(d => d.DeptId == roomAllocationModel.CourseDeptId).Single().DeptName);
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("classroom-allocation");
            }
            ViewBag.CourseDeptId = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            ViewBag.RoomAllocationCourseId = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Select Department First" } };
            ViewBag.RoomAllocationRoomId = new SelectList(db.Rooms.OrderBy(x => x.Room), "RoomId", "Room");
            ViewBag.RoomAllocationDayId = new SelectList(db.WeekDays.OrderBy(x => x.DayId), "DayId", "Day");
            return View(roomAllocationModel);
        }

        [ActionName("course-schedule")]
        public ActionResult CourseSchedule() {
            ViewBag.Departments = new SelectList(db.Departments.OrderBy(x => x.DeptCode), "DeptId", "Department");
            return View();
        }

        [ActionName("unassign-courses")]
        public ActionResult UnassignCourses()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("unassign-courses")]
        public ActionResult UnassignCourses(bool? unassign)
        {
            if (unassign != null)
            {
                if(new CourseManager().UnassignCourses())
                {
                    TempData["Message"] = "Teacher/Student courses unassigned";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("dashboard", "index");
                }
            }
            ViewBag.Message = "Courses unassigned failed! Try again. <br/> Or, there is no course found to unassign.";
            ViewBag.MessageType = "danger";
            return View();
        }

        [ActionName("unallocate-rooms")]
        public ActionResult UnallocateRooms()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("unallocate-rooms")]
        public ActionResult UnallocateRooms(bool? unallocate)
        {
            if (unallocate != null)
            {
                if (new CourseManager().UnallocateRooms())
                {
                    TempData["Message"] = "Teacher/Student courses unassigned";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("dashboard", "index");
                }
            }
            ViewBag.Message = "Room unallocation failed! Try again. <br/> Or, there is no room allocated yet.";
            ViewBag.MessageType = "danger";
            return View();
        }

        [HttpPost]
        public JsonResult GetCoursesSchedule(int deptId)
        {
            return Json(new BusinessLogics().CourseSchedule(deptId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsCourseCodeExists(string courseCode)
        {
            return Json(!db.Courses.Any(x => x.CourseCode == courseCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IscourseNameExists(string courseName)
        {
            return Json(!db.Courses.Any(x => x.CourseName == courseName), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCoursesByDeptId(int deptId)
        {
            return Json(db.Courses.Where(x=>x.CourseDeptId == deptId).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsTimeReversePlaced(string FromTime, string ToTime)
        {
            if (Convert.ToInt32(FromTime.Replace(":", "")) > Convert.ToInt32(ToTime.Replace(":", "")))
                return Json(false, JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult IsTimeConflicts(int RoomAllocationDayId, int RoomAllocationRoomId, string FromTime, string ToTime)
        {
            if (Convert.ToInt32(FromTime.Replace(":", "")) >= Convert.ToInt32(ToTime.Replace(":", "")))
                return Json("Start time should not greater than/equal to end time.", JsonRequestBehavior.AllowGet);

            var data = new BusinessLogics().IsTimeConflict(RoomAllocationDayId, RoomAllocationRoomId, FromTime, ToTime);
            return Json(data, JsonRequestBehavior.AllowGet);
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
