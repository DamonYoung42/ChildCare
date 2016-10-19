namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class driverphoneemailvalidationaddedtomodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false));
        }
    }
}
