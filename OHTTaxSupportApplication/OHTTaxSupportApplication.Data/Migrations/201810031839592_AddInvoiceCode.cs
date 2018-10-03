namespace OHTTaxSupportApplication.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "InvoiceCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "InvoiceCode");
        }
    }
}
