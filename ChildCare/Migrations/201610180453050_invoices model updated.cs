namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicesmodelupdated : DbMigration
    {

        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "UserId" });
            AlterColumn("dbo.Invoices", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Invoices", "UserId");
            AddForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "UserId" });
            AlterColumn("dbo.Invoices", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Invoices", "UserId");
            AddForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers", "Id");

            DropColumn("dbo.Invoices", "DatePaid");
        }
    }
}
