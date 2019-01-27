using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UoUWebApp.Models
{
    public class StudentCourseResultModel
    {
        public string StudentName { get; set; }
        public string StudentRegNo { get; set; }
        public string Department { get; set; }
        public string Semester { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
    }
}