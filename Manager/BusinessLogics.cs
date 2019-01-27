using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UoUWebApp.Context;
using UoUWebApp.Models;

namespace UoUWebApp.Manager
{
    public class BusinessLogics
    {
        public List<CourseModel> GetCoursesByDeptId(int deptId)
        {
            return (new UoUDBContext().Courses.Where(x => x.CourseDeptId == deptId)).ToList();
        }

        public bool IsTimeConflict(int dayId, int roomId, string fromTime, string toTime)
        {

            var from = "000" + (Convert.ToInt32(fromTime.Replace(":", "")) + 1);
            var to = "000" + (Convert.ToInt32(toTime.Replace(":", "")) - 1);

            string query = "SELECT * FROM RoomAllocations "+
                            "WHERE RoomAllocationDayId = @dayId "+
                            "AND RoomAllocationRoomId = @roomId "+
                            "AND RecordStatus = 1 "+
                            "AND((@from BETWEEN FromTime AND ToTime) OR(@to BETWEEN FromTime AND ToTime)) "+
                            "OR ((FromTime BETWEEN @from AND @to) OR (ToTime BETWEEN @from AND @to))";
            var data = new UoUDBContext().Database.SqlQuery<RoomAllocationModel>(
                                                    query,
                                                    new SqlParameter("dayId", dayId),
                                                    new SqlParameter("roomId", roomId),
                                                    new SqlParameter("from", from.Substring(from.Length - 4)),
                                                    new SqlParameter("to", to.Substring(to.Length - 4))).ToList();
            if (data.Count > 0)
                return false;
            return true;
        }

        public string FromTime2Time(string fromTime)
        {
            var hour = Convert.ToInt32(fromTime.Substring(0, 2));
            var minute = fromTime.Substring(2);
            var apm = (hour < 12) ? "AM" : "PM";
            hour = (hour <= 12) ? hour : (hour - 12);
            return string.Format("{0}:{1} {2}", hour, minute, apm);
        }

        public string ToTime2Time(string toTime)
        {
            var hour = Convert.ToInt32(toTime.Substring(0, 2));
            var minute = toTime.Substring(2);
            var apm = (hour < 12) ? "AM" : "PM";
            hour = (hour <= 12) ? hour : (hour - 12);
            return string.Format("{0}:{1} {2}", hour, minute, apm);
        }


        /*
         * Student registration number generator
         */
        public string RegistrationNo(string RegPart)
        {
            int total = new UoUDBContext().Database.SqlQuery<int>("SELECT COUNT(StudentRegNo) FROM dbo.Students WHERE StudentRegNo LIKE @RegPart", new SqlParameter("RegPart", RegPart+"%")).FirstOrDefault();
            string regNo = "000" + ( total + 1);
            return string.Format("{0}{1}", RegPart, regNo.Substring(regNo.Length - 4));
        }

        /*
         * List course schedules
         */
        public List<CourseSchedule> CourseSchedule(int deptId)
        {
            string query = "SELECT "+
                                "c.CourseCode, "+
                                "c.CourseName, "+
                                "STUFF((SELECT "+
                                "' Room No:' + r.Room + ', ' + w.Day + ', ' + "+
                                    "case "+
                                        "when cast(FromTime AS int) < 1200 then substring(FromTime, 1, 2) + ':' + substring(FromTime, 3, 2) + ' AM' " +
                                        "when cast(FromTime AS int) >= 1200 then substring(FromTime, 1, 2) + ':' + substring(FromTime, 3, 2) + ' PM' " +
                                    "end + "+
                                    "case "+
                                        "when cast(ToTime AS int) < 1200 then substring(ToTime, 1, 2) + ':' + substring(FromTime, 3, 2) + ' AM' " +
                                        "when cast(ToTime AS int) >= 1200 then substring(ToTime, 1, 2) + ':' + substring(FromTime, 3, 2) + ' PM' " +
                                    "end + '; <br/>' "+
                                    "FROM Rooms r "+
                                    "JOIN RoomAllocations ra ON r.RoomId = ra.RoomAllocationRoomId "+
                                    "JOIN WeekDays w ON w.DayId = ra.RoomAllocationDayId "+
                                    "WHERE ra.RoomAllocationCourseId = c.CourseId and ra.RecordStatus = 1 "+
                                    "FOR XML PATH('')), 1, 1, '') AS Schedules " +
                            "FROM Courses c "+
                            "JOIN RoomAllocations crs ON c.CourseId = crs.RoomAllocationCourseId "+
                            "WHERE crs.RecordStatus = 1 "+
                            "AND c.CourseDeptId = @deptId " +
                            "GROUP BY c.CourseId, c.CourseCode, c.CourseName "+

                            "UNION "+

                            "SELECT c1.CourseCode, c1.CourseName, 'Not scheduled yet' AS schedule FROM Courses c1 "+
                            "WHERE c1.CourseId NOT IN(SELECT RoomAllocationCourseId FROM RoomAllocations WHERE RecordStatus = 1 GROUP BY RoomAllocationCourseId) "+
                            "AND c1.CourseDeptId = @deptId";
                return new UoUDBContext().Database.SqlQuery<CourseSchedule>(query, new SqlParameter("deptId", deptId)).ToList();
        }

        /*
         * List department's active/assigned courses
         */
        public List<DepartmentActiveCourses> DepartmentActiveCourses(int deptId)
        {
            string query = "SELECT " +
                                "c.CourseId, " +
                                "c.CourseCode, " +
                                "c.CourseName, " +
                                "c.CourseCredit, " +
                                "c.CourseDeptId as 'DeptId', " +
                                "tc.TeacherCourseTeacherId as 'TeacherId', " +
                                "tc.RecordStatus " +
                            "FROM Courses c " +
                            "JOIN TeacherCourses tc on tc.TeacherCourseCourseId = c.CourseId " +
                            "WHERE tc.recordStatus = 1 AND c.CourseDeptId = @deptId";
            return new UoUDBContext().Database.SqlQuery<DepartmentActiveCourses>(query, new SqlParameter("deptId", deptId)).ToList();
        }


        /*
         * List selected department's course statics
         */
        public List<CourseStatics> CourseStatics(int deptId)
        {
            string query = "SELECT "+
                                "c.CourseCode, "+
                                "c.CourseName, "+
                                "s.Semester, "+
                                "t.TeacherName, "+
                                "d.DeptId "+
                            "FROM Courses c "+
                            "JOIN Semesters s ON c.CourseSemesterId = s.SemesterId "+
                            "JOIN TeacherCourses tc ON tc.TeacherCourseCourseId = c.CourseId "+
                            "JOIN Teachers t ON t.TeacherId = tc.TeacherCourseTeacherId "+
                            "JOIN Departments d ON d.DeptId = c.CourseDeptId "+
                            "WHERE tc.RecordStatus = 1 "+
                            "AND c.CourseDeptId = @deptId "+
                            "UNION " +
                            "SELECT "+
                                "c1.CourseCode, "+
                                "c1.CourseName, "+
                                "s1.Semester, "+
                                "'Not assigned yet' AS TeacherName, "+
                                "d1.DeptId "+
                            "FROM Courses c1 "+
                            "JOIN Semesters s1 ON c1.CourseSemesterId = s1.SemesterId "+
                            "JOIN Departments d1 ON c1.CourseDeptId = d1.deptId "+
                            "WHERE c1.courseId NOT IN (SELECT TeacherCourseCourseId FROM TeacherCourses WHERE RecordStatus = 1) "+
                            "AND c1.CourseDeptId = @deptId";
            return new UoUDBContext().Database.SqlQuery<CourseStatics>(query, new SqlParameter("deptId", deptId)).ToList();
        }
    }
}