namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aptnumbernotrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "AptNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "AptNumber", c => c.String(nullable: false));
        }
    }
}
