namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gradeleveladdedtoteachertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "GradeLevel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "GradeLevel");
        }
    }
}
