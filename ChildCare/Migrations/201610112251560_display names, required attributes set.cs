namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class displaynamesrequiredattributesset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "AptNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Children", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Children", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Children", "GradeLevel", c => c.String(nullable: false));
            AlterColumn("dbo.Children", "Photo", c => c.Binary(nullable: false));
            AlterColumn("dbo.Children", "Medications", c => c.String(nullable: false));
            AlterColumn("dbo.Children", "Notes", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Invoices", "Month", c => c.String(nullable: false));
            AlterColumn("dbo.PickupPersons", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.PickupPersons", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.PickupPersons", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.PickupPersons", "EmailAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickupPersons", "EmailAddress", c => c.String());
            AlterColumn("dbo.PickupPersons", "PhoneNumber", c => c.String());
            AlterColumn("dbo.PickupPersons", "LastName", c => c.String());
            AlterColumn("dbo.PickupPersons", "FirstName", c => c.String());
            AlterColumn("dbo.Invoices", "Month", c => c.String());
            AlterColumn("dbo.Teachers", "LastName", c => c.String());
            AlterColumn("dbo.Teachers", "FirstName", c => c.String());
            AlterColumn("dbo.Children", "Notes", c => c.String());
            AlterColumn("dbo.Children", "Medications", c => c.String());
            AlterColumn("dbo.Children", "Photo", c => c.Binary());
            AlterColumn("dbo.Children", "GradeLevel", c => c.String());
            AlterColumn("dbo.Children", "LastName", c => c.String());
            AlterColumn("dbo.Children", "FirstName", c => c.String());
            AlterColumn("dbo.Addresses", "State", c => c.String());
            AlterColumn("dbo.Addresses", "City", c => c.String());
            AlterColumn("dbo.Addresses", "Street", c => c.String());
            AlterColumn("dbo.Addresses", "AptNumber", c => c.String());
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String());
        }
    }
}
