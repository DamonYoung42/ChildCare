namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photopathastring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Children", "Photo", c => c.String());
            AlterColumn("dbo.PickupPersons", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickupPersons", "Photo", c => c.Binary());
            AlterColumn("dbo.Children", "Photo", c => c.Binary());
        }
    }
}
