using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("Students")]
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "You have to specify student name"),
            StringLength(100, ErrorMessage = "Student name should not more than 100 characters"),
            DisplayName("Name:"), 
            Column(TypeName = "varchar")]
        public string StudentName { get; set; }

        [StringLength(20),
            DisplayName("Registration No:"),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        public string StudentRegNo { get; set; }

        [Required(ErrorMessage = "You have to specify email address"),
            StringLength(100, ErrorMessage = "Email address should not more than 100 characters"),
            DisplayName("Email:"), 
            Column(TypeName = "varchar"),
            Index(IsUnique = true),
            DataType(DataType.EmailAddress), EmailAddress, Index(IsUnique = true),
            RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email")]
        [Remote("IsEmailExists", "student", HttpMethod = "POST", ErrorMessage = "Email address exists. Try another.")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "You have to specify contact no"),
            DisplayName("Contact No:"), 
            Column(TypeName = "varchar"),
            StringLength(15, MinimumLength = 5, ErrorMessage = "Contact number should be between 5 to 15 digits")]
        public string StudentContact { get; set; }

        [Required(ErrorMessage = "You have to specify registration date"),
            DisplayName("Registration Date:"),
            Column(TypeName = "datetime"),
            DataType(DataType.Date)]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessage = "You have to specify contact address"),
            DisplayName("Address:"), 
            Column(TypeName = "text"),
            StringLength(1000, ErrorMessage = "Address should not more than 1000 characters")]
        [DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }

        [Required(ErrorMessage = "You have to select a department"),
            DisplayName("Department:")]
        public int StudentDeptId { get; set; }

        [ForeignKey("StudentDeptId")]
        public virtual DepartmentModel DepartmentModel { get; set; }
    }
}