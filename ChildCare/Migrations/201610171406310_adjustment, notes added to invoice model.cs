namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustmentnotesaddedtoinvoicemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Adjustments", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Notes");
            DropColumn("dbo.Invoices", "Adjustments");
        }
    }
}
