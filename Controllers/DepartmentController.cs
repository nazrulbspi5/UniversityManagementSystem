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

namespace UoUWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private UoUDBContext db = new UoUDBContext();

        public async Task<ActionResult> Index()
        {
            string query = "SELECT DeptCode, DeptName, " +
                            "(SELECT count(*) FROM courses c WHERE c.CourseDeptId = d.deptId) AS 'TotalCourses', " +
                            "(SELECT count(*) FROM Teachers t WHERE t.teacherDeptId = d.deptId) AS 'TotalTeachers' " +
                            "FROM departments d order by DeptCode";
            var departments = new UoUDBContext().Database.SqlQuery<DepartmentStatics>(query).ToListAsync();
            return View(await departments);
        }

        public ActionResult Create()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DeptId,DeptCode,DeptName")] DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(departmentModel);
                if(await db.SaveChangesAsync() > 0)
                {
                    TempData["Message"] = string.Format("Department <b>{0} ({1})</b> is created successfully!", departmentModel.DeptName, departmentModel.DeptCode);
                    TempData["MessageType"] = "success";
                }
                return RedirectToAction("create");
            }

            return View(departmentModel);
        }

        [HttpPost]
        public JsonResult IsDeptCodeExists(string deptCode)
        {
            return Json(!db.Departments.Any(x => x.DeptCode == deptCode), JsonRequestBehavior.AllowGet);
            //var user = Membership.GetUser(UserName);
            //return Json(user == null);
        }

        [HttpPost]
        public JsonResult IsDeptNameExists(string deptName)
        {
            return Json(!db.Departments.Any(x => x.DeptName == deptName), JsonRequestBehavior.AllowGet);
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
