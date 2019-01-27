using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("Teachers")]
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "You have to specify teacher name"),
            StringLength(100, ErrorMessage = "Teacher name should not more than 100 characters"),
            DisplayName("Name"),
            Column(TypeName = "varchar")]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "You have to specify contact address"),
            StringLength(1000, ErrorMessage = "Address should not more than 1000 characters"),
            DisplayName("Address"),
            Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string TeacherAddress { get; set; }

        [Required(ErrorMessage = "You have to specify email address"),
            StringLength(100, ErrorMessage = "Email address should not more than 100 characters"),
            DisplayName("Email"), 
            Column(TypeName = "varchar"),
            DataType(DataType.EmailAddress), 
            EmailAddress, 
            Index(IsUnique = true),
            RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email")]
        [Remote("IsEmailExists", "Teacher", HttpMethod = "POST", ErrorMessage = "Email address exists. Try another")]
        public string TeacherEmail { get; set; }

        [Required(ErrorMessage = "You have to specify contact no"),
            StringLength(15, MinimumLength = 5, ErrorMessage = "Contact number should be between 5 to 15 digits"),
            DisplayName("Contact No"), 
            Column(TypeName = "varchar")]
        public string TeacherContact { get; set; }

        [Required(ErrorMessage = "You have to select a designation"),
            DisplayName("Designation")]
        public int TeacherDesignationId { get; set; }

        [ForeignKey("TeacherDesignationId")]
        public virtual DesignationModel DesignationModel { get; set; }

        [Required(ErrorMessage = "You have to select a department")]
        [DisplayName("Department")]
        public int TeacherDeptId { get; set; }

        [ForeignKey("TeacherDeptId")]
        public virtual DepartmentModel DepartmentModel { get; set; }

        [Required(ErrorMessage = "You have to specify taken credits")]
        [DisplayName("Total Credits"), Column(TypeName = "float")]
        [Range(0.5, Double.MaxValue, ErrorMessage = "Credit should be at least 0.5")]
        public float TotalCredit { get; set; }

        [NotMapped]
        public virtual List<TeacherCourseModel> TeacherCourseModel { get; set; }
    }
}