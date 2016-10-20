namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountDuenowBilledAmountoninvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "BilledAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "AmountDue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "AmountDue", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "BilledAmount");
        }
    }
}
