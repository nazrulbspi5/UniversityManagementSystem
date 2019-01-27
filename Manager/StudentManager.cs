using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UoUWebApp.Context;
using UoUWebApp.Models;

namespace UoUWebApp.Manager
{
    public class StudentManager
    {
        public List<StudentModel> GetStudents()
        {
            return new UoUDBContext().Students.ToList();
        }

        public StudentModel GetStudentById(int studentId)
        {
            return new UoUDBContext().Students.Where(x => x.StudentId == studentId).Single();
        }

        public List<StudentModel> GetStudentsByDeptId(int deptId)
        {
            return new UoUDBContext().Students.Where(x => x.StudentDeptId == deptId).ToList();
        }

        public List<StudentModel> GetStudentsOrderByRegNo()
        {
            return new UoUDBContext().Students.OrderBy(x => x.StudentRegNo).ToList();
        }

        public List<GradeModel> GetGrades()
        {
            return new UoUDBContext().Grades.OrderBy(x => x.GradeId).ToList();
        }

        public List<StudentCourseResultModel> GetResult(int studentId)
        {
            var query = "SELECT "+
                "s.StudentName, s.StudentRegNo, "+
                "d.DeptName + ' (' + d.DeptCode + ')' as Department, " +
                "c.CourseCode, c.CourseName, a.Semester, " +
                "(IsNull(CAST(sc.Grade AS varchar(20)), 'Not Graded Yet')) AS Grade "+
                "FROM Courses c JOIN StudentCourses sc ON c.CourseId = sc.StudentCourseCourseId "+
                "JOIN Students s ON s.StudentId = sc.StudentCourseStudentId "+
                "JOIN departments d ON s.studentDeptId = d.deptId "+
                "JOIN Semesters a ON a.SemesterId = c.coursesemesterid "+
                "WHERE sc.RecordStatus = 1 AND sc.StudentCourseStudentId = @studentId "+
                "ORDER BY Grade";
            return new UoUDBContext().Database.SqlQuery<StudentCourseResultModel>(query, new SqlParameter("studentId", studentId)).ToList();
        }
    }
}