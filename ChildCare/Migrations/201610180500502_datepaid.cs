namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datepaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "DatePaid", c => c.DateTime(nullable: true));

        }
        public override void Down()
        {
        }
    }
}
