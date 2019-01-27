using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UoUWebApp.Context;
using UoUWebApp.Gateway;
using UoUWebApp.Models;

namespace UoUWebApp.Manager
{
    public class CourseManager
    {
        public CourseModel GetCourseById(int courseId)
        {
            return new UoUDBContext().Courses.Where(x => x.CourseId == courseId).Single();
        }

        public List<CourseModel> GetCoursesByDepartment(int deptId)
        {
            return new UoUDBContext().Courses.Where(x => x.CourseDeptId == deptId).OrderBy(y => y.CourseCode).ToList();
        }

        public List<CourseModel> GetCoursesByStudentId(int studentId)
        {
            var studentCourses = new UoUDBContext().StudentCourses.Where(x => x.StudentCourseStudentId == studentId && x.RecordStatus == 1).Select(y => y.StudentCourseCourseId).ToList();
            return new UoUDBContext().Courses.Where(x => studentCourses.Contains(x.CourseId)).OrderBy(y => y.CourseCode).ToList();
        }

        public bool UnassignCourses() {
            UoUDBContext db = new UoUDBContext();
            foreach (var teacher_course in db.TeacherCourses.Where(x => x.RecordStatus == 1).ToList())
            {
                teacher_course.RecordStatus = 0;
                db.Entry(teacher_course).State = EntityState.Modified;
            }

            foreach (var student_course in db.StudentCourses.Where(x => x.RecordStatus == 1).ToList())
            {
                student_course.RecordStatus = 0;
                db.Entry(student_course).State = EntityState.Modified;
            }
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool UnallocateRooms()
        {
            UoUDBContext db = new UoUDBContext();
            foreach (var classroom in db.RoomAllocations.Where(x => x.RecordStatus == 1).ToList())
            {
                classroom.RecordStatus = 0;
                db.Entry(classroom).State = EntityState.Modified;
            }
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}