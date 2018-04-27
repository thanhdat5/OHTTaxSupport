namespace OHTTaxSupportApplication.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Age", c => c.String());
            AddColumn("dbo.User", "Address", c => c.String(maxLength: 500));
            AddColumn("dbo.User", "AboutMe", c => c.String(maxLength: 250));
            AddColumn("dbo.User", "LastOnline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "LastOnline");
            DropColumn("dbo.User", "AboutMe");
            DropColumn("dbo.User", "Address");
            DropColumn("dbo.User", "Age");
        }
    }
}
