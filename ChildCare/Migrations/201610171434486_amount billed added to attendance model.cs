namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountbilledaddedtoattendancemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "AmountBilled", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "AmountBilled");
        }
    }
}
