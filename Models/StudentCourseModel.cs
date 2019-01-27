using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("StudentCourses")]
    public class StudentCourseModel
    {
        [Key]
        public int SCId { get; set; }

        [Required(ErrorMessage = "You have to select student"),
            DisplayName("Student Registration No:")]
        public int StudentCourseStudentId { get; set; }

        [ForeignKey("StudentCourseStudentId")]
        public virtual StudentModel StudentModel { get; set; }

        [Required(ErrorMessage = "You have to select a course"),
            DisplayName("Course:")]
        public int StudentCourseCourseId { get; set; }

        [ForeignKey("StudentCourseCourseId")]
        public virtual CourseModel CourseModel { get; set; }

        [Required(ErrorMessage = "You have to specify course enroll date"),
            DisplayName("Enroll Date:"), 
            Column(TypeName = "datetime"), 
            DataType(DataType.DateTime),
            DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Remote("IsEnrollDateOverlapRegDate", "student", HttpMethod = "POST", ErrorMessage = "Enroll date overlaps student registration date.", AdditionalFields = "StudentCourseStudentId, EnrollDate")]
        public DateTime EnrollDate { get; set; }

        [DisplayName("Grade:"),
            Column(TypeName ="varchar"),
            StringLength(2, ErrorMessage = "Length should not more than 2 characters long.")]
        [Remote("IsGradeEmpty", "student", HttpMethod = "POST", ErrorMessage = "Please, select a grade.")]
        public string Grade { get; set; }
        //public int StudentCourseGradeId { get; set; }

        //[ForeignKey("StudentCourseGradeId")]
        //public virtual GradeModel GradeModel { get; set; }

        [Display(AutoGenerateField = false)]
        public int RecordStatus { get; set; }
    }
}