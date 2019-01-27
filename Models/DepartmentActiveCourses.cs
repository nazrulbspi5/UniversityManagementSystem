using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UoUWebApp.Models
{
    public class DepartmentActiveCourses
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double CourseCredit { get; set; }
        public int DeptId { get; set; }
        public int TeacherId { get; set; }
        public int RecordStatus { get; set; }
    }
}