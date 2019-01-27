namespace UoUWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false, maxLength: 7, unicode: false),
                        CourseName = c.String(nullable: false, maxLength: 100, unicode: false),
                        CourseCredit = c.Double(nullable: false),
                        CourseDesc = c.String(nullable: false, unicode: false, storeType: "text"),
                        CourseDeptId = c.Int(nullable: false),
                        CourseSemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.CourseDeptId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.CourseSemesterId, cascadeDelete: true)
                .Index(t => t.CourseCode, unique: true)
                .Index(t => t.CourseName, unique: true)
                .Index(t => t.CourseDeptId)
                .Index(t => t.CourseSemesterId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptId = c.Int(nullable: false, identity: true),
                        DeptCode = c.String(nullable: false, maxLength: 7, unicode: false),
                        DeptName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.DeptId)
                .Index(t => t.DeptCode, unique: true)
                .Index(t => t.DeptName, unique: true);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Int(nullable: false, identity: true),
                        Semester = c.String(nullable: false, maxLength: 11, unicode: false),
                    })
                .PrimaryKey(t => t.SemesterId)
                .Index(t => t.Semester, unique: true);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Designation = c.String(nullable: false, maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        Grade = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.GradeId)
                .Index(t => t.Grade, unique: true);
            
            CreateTable(
                "dbo.RoomAllocations",
                c => new
                    {
                        AllocationId = c.Long(nullable: false, identity: true),
                        RoomAllocationCourseId = c.Int(nullable: false),
                        RoomAllocationRoomId = c.Int(nullable: false),
                        RoomAllocationDayId = c.Int(nullable: false),
                        FromTime = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ToTime = c.String(nullable: false, maxLength: 8000, unicode: false),
                        RecordStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AllocationId)
                .ForeignKey("dbo.Courses", t => t.RoomAllocationCourseId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomAllocationRoomId, cascadeDelete: true)
                .ForeignKey("dbo.WeekDays", t => t.RoomAllocationDayId, cascadeDelete: true)
                .Index(t => t.RoomAllocationCourseId)
                .Index(t => t.RoomAllocationRoomId)
                .Index(t => t.RoomAllocationDayId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Room = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .Index(t => t.Room, unique: true);
            
            CreateTable(
                "dbo.WeekDays",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Day = c.String(nullable: false, maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        SCId = c.Int(nullable: false, identity: true),
                        StudentCourseStudentId = c.Int(nullable: false),
                        StudentCourseCourseId = c.Int(nullable: false),
                        EnrollDate = c.DateTime(nullable: false),
                        Grade = c.String(nullable: false, maxLength: 2, unicode: false),
                        RecordStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SCId)
                .ForeignKey("dbo.Courses", t => t.StudentCourseCourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentCourseStudentId, cascadeDelete: false)
                .Index(t => t.StudentCourseStudentId)
                .Index(t => t.StudentCourseCourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 100, unicode: false),
                        StudentRegNo = c.String(maxLength: 20, unicode: false),
                        StudentEmail = c.String(nullable: false, maxLength: 100, unicode: false),
                        StudentContact = c.String(nullable: false, maxLength: 15, unicode: false),
                        RegDate = c.DateTime(nullable: false),
                        StudentAddress = c.String(nullable: false, unicode: false, storeType: "text"),
                        StudentDeptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.StudentDeptId, cascadeDelete: true)
                .Index(t => t.StudentRegNo, unique: true)
                .Index(t => t.StudentEmail, unique: true)
                .Index(t => t.StudentDeptId);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        TCId = c.Int(nullable: false, identity: true),
                        TeacherCourseTeacherId = c.Int(nullable: false),
                        TeacherCourseCourseId = c.Int(nullable: false),
                        RecordStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TCId)
                .ForeignKey("dbo.Courses", t => t.TeacherCourseCourseId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherCourseTeacherId, cascadeDelete: false)
                .Index(t => t.TeacherCourseTeacherId)
                .Index(t => t.TeacherCourseCourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false, maxLength: 100, unicode: false),
                        TeacherAddress = c.String(nullable: false, unicode: false, storeType: "text"),
                        TeacherEmail = c.String(nullable: false, maxLength: 100, unicode: false),
                        TeacherContact = c.String(nullable: false, maxLength: 15, unicode: false),
                        TeacherDesignationId = c.Int(nullable: false),
                        TeacherDeptId = c.Int(nullable: false),
                        TotalCredit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.TeacherDeptId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.TeacherDesignationId, cascadeDelete: true)
                .Index(t => t.TeacherEmail, unique: true)
                .Index(t => t.TeacherDesignationId)
                .Index(t => t.TeacherDeptId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "TeacherCourseTeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "TeacherDesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "TeacherDeptId", "dbo.Departments");
            DropForeignKey("dbo.TeacherCourses", "TeacherCourseCourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "StudentCourseStudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentDeptId", "dbo.Departments");
            DropForeignKey("dbo.StudentCourses", "StudentCourseCourseId", "dbo.Courses");
            DropForeignKey("dbo.RoomAllocations", "RoomAllocationDayId", "dbo.WeekDays");
            DropForeignKey("dbo.RoomAllocations", "RoomAllocationRoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomAllocations", "RoomAllocationCourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CourseSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "CourseDeptId", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "TeacherDeptId" });
            DropIndex("dbo.Teachers", new[] { "TeacherDesignationId" });
            DropIndex("dbo.Teachers", new[] { "TeacherEmail" });
            DropIndex("dbo.TeacherCourses", new[] { "TeacherCourseCourseId" });
            DropIndex("dbo.TeacherCourses", new[] { "TeacherCourseTeacherId" });
            DropIndex("dbo.Students", new[] { "StudentDeptId" });
            DropIndex("dbo.Students", new[] { "StudentEmail" });
            DropIndex("dbo.Students", new[] { "StudentRegNo" });
            DropIndex("dbo.StudentCourses", new[] { "StudentCourseCourseId" });
            DropIndex("dbo.StudentCourses", new[] { "StudentCourseStudentId" });
            DropIndex("dbo.Rooms", new[] { "Room" });
            DropIndex("dbo.RoomAllocations", new[] { "RoomAllocationDayId" });
            DropIndex("dbo.RoomAllocations", new[] { "RoomAllocationRoomId" });
            DropIndex("dbo.RoomAllocations", new[] { "RoomAllocationCourseId" });
            DropIndex("dbo.Grades", new[] { "Grade" });
            DropIndex("dbo.Semesters", new[] { "Semester" });
            DropIndex("dbo.Departments", new[] { "DeptName" });
            DropIndex("dbo.Departments", new[] { "DeptCode" });
            DropIndex("dbo.Courses", new[] { "CourseSemesterId" });
            DropIndex("dbo.Courses", new[] { "CourseDeptId" });
            DropIndex("dbo.Courses", new[] { "CourseName" });
            DropIndex("dbo.Courses", new[] { "CourseCode" });
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.Students");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.WeekDays");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomAllocations");
            DropTable("dbo.Grades");
            DropTable("dbo.Designations");
            DropTable("dbo.Semesters");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
        }
    }
}
