namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billingstableupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Billings", "SuiteNumber", c => c.String());
            DropColumn("dbo.Billings", "AptNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Billings", "AptNumber", c => c.String());
            DropColumn("dbo.Billings", "SuiteNumber");
        }
    }
}
