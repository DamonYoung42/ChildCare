namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class einaddressinfoaddedtobillingstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Billings", "FederalEIN", c => c.String(nullable: false));
            AddColumn("dbo.Billings", "StreetNumber", c => c.String(nullable: false));
            AddColumn("dbo.Billings", "AptNumber", c => c.String());
            AddColumn("dbo.Billings", "Street", c => c.String(nullable: false));
            AddColumn("dbo.Billings", "State", c => c.String(nullable: false));
            AddColumn("dbo.Billings", "ZipCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Billings", "ZipCode");
            DropColumn("dbo.Billings", "State");
            DropColumn("dbo.Billings", "Street");
            DropColumn("dbo.Billings", "AptNumber");
            DropColumn("dbo.Billings", "StreetNumber");
            DropColumn("dbo.Billings", "FederalEIN");
        }
    }
}
