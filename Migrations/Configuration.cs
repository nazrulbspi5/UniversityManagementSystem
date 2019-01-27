namespace UoUWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<UoUWebApp.Context.UoUDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UoUWebApp.Context.UoUDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            context.Semesters.AddOrUpdate(
                    new Models.SemesterModel { SemesterId = 1, Semester = "SEMESTER-01" },
                    new Models.SemesterModel { SemesterId = 2, Semester = "SEMESTER-02" },
                    new Models.SemesterModel { SemesterId = 3, Semester = "SEMESTER-03" },
                    new Models.SemesterModel { SemesterId = 4, Semester = "SEMESTER-04" },
                    new Models.SemesterModel { SemesterId = 5, Semester = "SEMESTER-05" },
                    new Models.SemesterModel { SemesterId = 6, Semester = "SEMESTER-06" },
                    new Models.SemesterModel { SemesterId = 7, Semester = "SEMESTER-07" },
                    new Models.SemesterModel { SemesterId = 8, Semester = "SEMESTER-08" }
                );

            context.Rooms.AddOrUpdate(
                    new Models.RoomModel { RoomId = 1, Room = "101" }, 
                    new Models.RoomModel { RoomId = 2, Room = "102" },
                    new Models.RoomModel { RoomId = 3, Room = "103" }, 
                    new Models.RoomModel { RoomId = 4, Room = "104" },
                    new Models.RoomModel { RoomId = 5, Room = "105" }, 
                    new Models.RoomModel { RoomId = 6, Room = "106" },
                    new Models.RoomModel { RoomId = 7, Room = "107" }, 
                    new Models.RoomModel { RoomId = 8, Room = "108" },
                    new Models.RoomModel { RoomId = 9, Room = "109" }, 
                    new Models.RoomModel { RoomId = 10, Room = "110" },
                    new Models.RoomModel { RoomId = 11, Room = "201" }, 
                    new Models.RoomModel { RoomId = 12, Room = "202" },
                    new Models.RoomModel { RoomId = 13, Room = "203" }, 
                    new Models.RoomModel { RoomId = 14, Room = "204" },
                    new Models.RoomModel { RoomId = 15, Room = "205" }, 
                    new Models.RoomModel { RoomId = 16, Room = "206" },
                    new Models.RoomModel { RoomId = 17, Room = "207" }, 
                    new Models.RoomModel { RoomId = 18, Room = "208" },
                    new Models.RoomModel { RoomId = 19, Room = "209" }, 
                    new Models.RoomModel { RoomId = 20, Room = "210" }
                );

            context.WeekDays.AddOrUpdate(
                    new Models.WeekDayModel { DayId = 1, Day = "SAT" },
                    new Models.WeekDayModel { DayId = 2, Day = "SUN" },
                    new Models.WeekDayModel { DayId = 3, Day = "MON" },
                    new Models.WeekDayModel { DayId = 4, Day = "TUE" },
                    new Models.WeekDayModel { DayId = 5, Day = "WED" },
                    new Models.WeekDayModel { DayId = 6, Day = "THU" },
                    new Models.WeekDayModel { DayId = 7, Day = "FRI" }
                );

            context.Designations.AddOrUpdate(
                    new Models.DesignationModel { DesignationId = 1, Designation = "Professor" },
                    new Models.DesignationModel { DesignationId = 2, Designation = "Asst. Professor" },
                    new Models.DesignationModel { DesignationId = 3, Designation = "Assoc. Professor" },
                    new Models.DesignationModel { DesignationId = 4, Designation = "Senior Lecturer" },
                    new Models.DesignationModel { DesignationId = 5, Designation = "Lecturer" },
                    new Models.DesignationModel { DesignationId = 6, Designation = "Trainer/Instructor" }
                );

            context.Grades.AddOrUpdate(
                    new Models.GradeModel { GradeId = 1, Grade = "A+" },
                    new Models.GradeModel { GradeId = 2, Grade = "A" },
                    new Models.GradeModel { GradeId = 3, Grade = "A-" },
                    new Models.GradeModel { GradeId = 4, Grade = "B+" },
                    new Models.GradeModel { GradeId = 5, Grade = "B" },
                    new Models.GradeModel { GradeId = 6, Grade = "B-" },
                    new Models.GradeModel { GradeId = 7, Grade = "C+" },
                    new Models.GradeModel { GradeId = 8, Grade = "C" },
                    new Models.GradeModel { GradeId = 9, Grade = "C-" },
                    new Models.GradeModel { GradeId = 10, Grade = "D+" },
                    new Models.GradeModel { GradeId = 11, Grade = "D" },
                    new Models.GradeModel { GradeId = 12, Grade = "D-" },
                    new Models.GradeModel { GradeId = 13, Grade = "F" }
                );

            context.Departments.AddOrUpdate(
                    new Models.DepartmentModel { DeptId = 1, DeptCode = "ACC", DeptName = "Accounting" },
                    new Models.DepartmentModel { DeptId = 2, DeptCode = "MGT", DeptName = "Management" },
                    new Models.DepartmentModel { DeptId = 3, DeptCode = "FIN", DeptName = "Finance" },
                    new Models.DepartmentModel { DeptId = 4, DeptCode = "BNK", DeptName = "Banking" },
                    new Models.DepartmentModel { DeptId = 5, DeptCode = "MRK", DeptName = "Marketing" },
                    new Models.DepartmentModel { DeptId = 6, DeptCode = "ECO", DeptName = "Economics" },
                    new Models.DepartmentModel { DeptId = 7, DeptCode = "STT", DeptName = "Statistics" }
                );

            context.Courses.AddOrUpdate(
                    new Models.CourseModel { CourseId = 1, CourseCode = "ACC-001", CourseName = "Basic Accounting", CourseCredit = 1, CourseDesc = "course description...", CourseDeptId = 1, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 2, CourseCode = "ACC-002", CourseName = "Intermediate Accounting", CourseCredit = 2, CourseDesc = "course description...", CourseDeptId = 1, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 3, CourseCode = "ACC-003", CourseName = "Advance Accounting", CourseCredit = 3, CourseDesc = "course description...", CourseDeptId = 1, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 4, CourseCode = "ACC-004", CourseName = "Cost Accounting", CourseCredit = 1, CourseDesc = "course description...", CourseDeptId = 1, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 5, CourseCode = "ACC-005", CourseName = "Management Accounting", CourseCredit = 4, CourseDesc = "course description...", CourseDeptId = 1, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 6, CourseCode = "MGT-001", CourseName = "Introduction to Management", CourseCredit = 1, CourseDesc = "course description...", CourseDeptId = 2, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 7, CourseCode = "MGT-002", CourseName = "Managerial Accounting", CourseCredit = 3, CourseDesc = "course description...", CourseDeptId = 2, CourseSemesterId = 1 },
                    new Models.CourseModel { CourseId = 8, CourseCode = "MGT-003", CourseName = "Office Management", CourseCredit = 4, CourseDesc = "course description...", CourseDeptId = 2, CourseSemesterId = 1 }
                );
        }
    }
}
