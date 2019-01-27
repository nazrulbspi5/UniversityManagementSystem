using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("TeacherCourses")]
    public class TeacherCourseModel
    {
        [Key]
        public int TCId { get; set; }

        [Required(ErrorMessage = "You have to select Department"),
            DisplayName("Department:")]
        [NotMapped]
        public int TeacherCourseDeptId { get; set; }

        [NotMapped]
        public virtual DepartmentModel DepartmentModel { get; set; }

        [Required(ErrorMessage = "You have to select Teacher"),
            DisplayName("Teacher:")]
        public int TeacherCourseTeacherId { get; set; }

        [ForeignKey("TeacherCourseTeacherId")]
        public virtual TeacherModel TeacherModel { get; set; }

        [Required(ErrorMessage = "You have to select a course"),
            DisplayName("Course:")]
        public int TeacherCourseCourseId { get; set; }

        [ForeignKey("TeacherCourseCourseId")]
        public virtual CourseModel CourseModel { get; set; }

        [Display(AutoGenerateField = false), 
            DefaultValue(1),
            DisplayName("Record Status:")]
        public int RecordStatus { get; set; }
    }
}