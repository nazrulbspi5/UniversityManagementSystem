using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UoUWebApp.Models;

namespace UoUWebApp.Gateway
{
    public class CourseGateway : DatabaseGateway
    {
        private CourseModel GetCourse()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                reader.Read();
                return new CourseModel
                {
                    CourseId = Convert.ToInt32(reader["CourseId"]),
                    CourseCode = reader["CourseCode"].ToString(),
                    CourseName = reader["CourseName"].ToString(),
                    CourseDesc = reader["CourseDesc"].ToString(),
                    CourseCredit = float.Parse(reader["CourseCredit"].ToString()),
                    CourseDeptId = Convert.ToInt32(reader["CourseDeptId"]),
                    CourseSemesterId = Convert.ToInt32(reader["CourseSemesterId"])
                };
            }
            return null;
        }

        private List<CourseModel> GetCourses()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                List<CourseModel> courses = new List<CourseModel>();
                while (reader.Read()) {
                    courses.Add(new CourseModel
                    {
                        CourseId = Convert.ToInt32(reader["CourseId"]),
                        CourseCode = reader["CourseCode"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        CourseDesc = reader["CourseDesc"].ToString(),
                        CourseCredit = float.Parse(reader["CourseCredit"].ToString()),
                        CourseDeptId = Convert.ToInt32(reader["CourseDeptId"]),
                        CourseSemesterId = Convert.ToInt32(reader["CourseSemesterId"])
                    });
                }
                return courses;
                
            }
            return null;
        }

        public List<CourseModel> GetAllCoursesByDeptId(int deptId)
        {
            command.CommandText = "SELECT * FROM Courses WHERE CourseDeptId = @deptId";
            command.Parameters.Clear();
            command.Parameters.Add("deptId", SqlDbType.Int).Value = deptId;

            return GetCourses();
        }

        internal List<CourseModel> GetCoursesByTeacherId(int teacherId)
        {
            command.CommandText = "SELECT * FROM Courses WHERE CourseId IN (SELECT TeacherCourseCourseId FROM TeacherCourses WHERE RecordStatus = 1 AND TeacherCourseTeacherId = @teacherId)";
            command.Parameters.Clear();
            command.Parameters.Add("teacherId", SqlDbType.Int).Value = teacherId;

            return GetCourses();
        }
    }
}