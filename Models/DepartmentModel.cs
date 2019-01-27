using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("Departments")]
    public class DepartmentModel
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "You have to specify department code"),
            StringLength(7, MinimumLength = 2, ErrorMessage = "Department code must be between 2 to 7 characters long"),
            DisplayName("Department Code:"),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        [Remote("IsDeptCodeExists", "Department", HttpMethod = "POST", ErrorMessage = "Department code exists. Try another")]
        public string DeptCode { get; set; }

        [Required(ErrorMessage = "You have to specify department name"),
            DisplayName("Department Name:"),
            StringLength(100),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        [Remote("IsDeptNameExists", "Department", HttpMethod = "POST", ErrorMessage = "Department name exists. Try another")]
        public string DeptName { get; set; }

        /*
         * Just to show in front-end in drop down list
         */
        [NotMapped]
        public string Department { get { return DeptCode + " - " + DeptName; } }

        //public virtual List<CourseModel> CourseModel { get; set; }
        //public virtual List<TeacherModel> TeacherModel { get; set; }
        //public virtual List<StudentModel> StudentModel { get; set; }
    }
}