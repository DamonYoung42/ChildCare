namespace ChildCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identitymodelphotostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Photo", c => c.Binary());
        }
    }
}
