namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicationnotesaddedtochildtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "Medications", c => c.String());
            AddColumn("dbo.Children", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Children", "Notes");
            DropColumn("dbo.Children", "Medications");
        }
    }
}
