using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UoUWebApp.Context;

namespace UoUWebApp.Models
{
    public class CourseStatics
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string TeacherName { get; set; }
        public int DeptId { get; set; }

        //public static List<CourseStatics> GetCourseStatics() {
        //    string query = "SELECT" +
        //                    "c.CourseCode, " +
        //                    "c.CourseName, " +
        //                    "s.Semester, " +
        //                    "t.TeacherName, " +
        //                    "d.DeptId " +
        //                    "FROM Courses c " +
        //                    "JOIN Semesters s ON c.CourseSemesterId = s.SemesterId " +
        //                    "JOIN TeacherCourses tc ON tc.TeacherCourseCourseId = c.CourseId " +
        //                    "JOIN Teachers t ON t.TeacherId = tc.TeacherCourseTeacherId " +
        //                    "JOIN Departments d ON d.DeptId = c.CourseDeptId " +
        //                    "WHERE tc.RecordStatus = 1";
        //    return new UoUDBContext().Database.SqlQuery<CourseStatics>(query).ToList();
        //}
    }
}