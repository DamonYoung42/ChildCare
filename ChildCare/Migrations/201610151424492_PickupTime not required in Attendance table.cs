namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickupTimenotrequiredinAttendancetable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendances", "PickupTime", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.Attendances", "PickupTime", c => c.DateTime(nullable: false));
        }
    }
}
