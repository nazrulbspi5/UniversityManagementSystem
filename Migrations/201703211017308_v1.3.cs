namespace UoUWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentCourses", "Grade", c => c.String(maxLength: 2, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentCourses", "Grade", c => c.String(nullable: false, maxLength: 2, unicode: false));
        }
    }
}
