using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    public class DepartmentStatics
    {
        [DisplayName("Department Code")]
        public string DeptCode { get; set; }

        [DisplayName("Department Name")]
        public string DeptName { get; set; }

        [DisplayName("Total Courses")]
        public int TotalCourses { get; set; }

        [DisplayName("Total Teachers")]
        public int TotalTeachers { get; set; }
    }
}