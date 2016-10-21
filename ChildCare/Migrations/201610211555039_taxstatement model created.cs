namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taxstatementmodelcreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxStatements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxStatements", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TaxStatements", new[] { "UserId" });
            DropTable("dbo.TaxStatements");
        }
    }
}
