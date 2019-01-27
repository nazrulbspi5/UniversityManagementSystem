using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("Courses")]
    public class CourseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "You have to specify course code"),
            StringLength(7, MinimumLength = 7, ErrorMessage = "Course code must be 7 characters long"),
            DisplayName("Course Code:"),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        [Remote("IsCourseCodeExists", "Course", HttpMethod = "POST", ErrorMessage = "Course code exists. Try another")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "You have to specify course name"),
            StringLength(100, ErrorMessage = "Course name should not more than 100 characters long"),
            DisplayName("Course Name:"),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        [Remote("IsCourseNameExists", "Course", HttpMethod = "POST", ErrorMessage = "Course name exists. Try another")]
        public string CourseName { get; set; }

        /*
         * Just to show in front-end in drop down list
         */
        [NotMapped]
        public string Course { get { return CourseCode + " - " + CourseName; } }

        [Required(ErrorMessage = "You have to specify course credit"),
            Range(0.5, 5.0, ErrorMessage = "You have to input between 0.5 to 5.0"),
            DisplayName("Course Credit:"),
            Column(TypeName = "float")]
        public float CourseCredit { get; set; }

        [Required(ErrorMessage = "You have to specify course description"),
            StringLength(1000, ErrorMessage = "Description must be not more that 1000 characters long"),
            DisplayName("Course Desc:"),
            Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string CourseDesc { get; set; }

        [Required(ErrorMessage = "You have to select a department"),
            DisplayName("Department:")]
        public int CourseDeptId { get; set; }

        [ForeignKey("CourseDeptId")]
        public virtual DepartmentModel DepartmentModel { get; set; }

        [Required(ErrorMessage = "You have to select semester"),
            DisplayName("Semester:")]
        public int CourseSemesterId { get; set; }

        [ForeignKey("CourseSemesterId")]
        public virtual SemesterModel SemesterModel { get; set; }
    }
}