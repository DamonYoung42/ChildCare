namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gradelevelnotrequiredonchild : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Children", "GradeLevel", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Children", "GradeLevel", c => c.String(nullable: false));
        }
    }
}
