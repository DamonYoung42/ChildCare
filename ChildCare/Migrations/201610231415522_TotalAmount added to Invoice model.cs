namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalAmountaddedtoInvoicemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "TotalAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "TotalAmount");
        }
    }
}
