namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickupPersonchangedtoDriver : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PickupPersons", newName: "Drivers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Drivers", newName: "PickupPersons");
        }
    }
}
