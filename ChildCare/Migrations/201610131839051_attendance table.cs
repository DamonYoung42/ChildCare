namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendancetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
    "dbo.Attendances",
    c => new
    {
        Id = c.Int(nullable: false, identity: true),
        Date = c.DateTime(nullable: false),
        PickupTime = c.DateTime(nullable: false),
        ChildId = c.Int(nullable: false),
        start = c.DateTime(nullable: true),
        end = c.DateTime(nullable: true),
        title = c.String(nullable: true),
        editable = c.Boolean(nullable: true),
        allDay = c.Boolean(nullable: true)
    })
    .PrimaryKey(t => t.Id)
    .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: true)
    .Index(t => t.ChildId);
        }
        
        public override void Down()
        {
        }
    }
}
