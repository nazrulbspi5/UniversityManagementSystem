using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UoUWebApp.Models;

namespace UoUWebApp.Context
{
    public class UoUDBContext : DbContext
    {
        /*
         * DbContext class's SaveChanges method overrides 
         * using following method to check which validation 
         * rules conflicts with data insertion in 
         * Package Manager Console :)
         */
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //Below is also working code to output errors while migrating...
                //var sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                //throw new DbEntityValidationException(
                //    "Entity Validation Failed - errors follow:\n" +
                //    sb.ToString(), ex
                //    ); // Add the original exception as the innerException
            }
        }

        /* 
         * Currently not required as this class name
         * and connection string name in web.config are same.
         */
        //public UoUDBContext() : base("UoUDBContext") { }

        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<SemesterModel> Semesters { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<WeekDayModel> WeekDays { get; set; }
        public DbSet<RoomAllocationModel> RoomAllocations { get; set; }
        public DbSet<DesignationModel> Designations { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<TeacherCourseModel> TeacherCourses { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<StudentCourseModel> StudentCourses { get; set; }
    }
}