namespace OHTTaxSupportApplication.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountCode = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        CategoryID = c.Int(nullable: false),
                        TaxValueID = c.Int(nullable: false),
                        SH = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.TaxValue", t => t.TaxValueID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.TaxValueID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InvoiceDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceID = c.Int(nullable: false),
                        Value = c.Decimal(precision: 18, scale: 2),
                        DepartmentID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        TaxValueID = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        Product_ID = c.Int(),
                        Unit_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.TaxValue", t => t.TaxValueID, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.InvoiceID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .ForeignKey("dbo.Unit", t => t.Unit_ID)
                .Index(t => t.InvoiceID)
                .Index(t => t.DepartmentID)
                .Index(t => t.CategoryID)
                .Index(t => t.TaxValueID)
                .Index(t => t.Product_ID)
                .Index(t => t.Unit_ID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 250),
                        CompanyID = c.Int(nullable: false),
                        Address = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 250),
                        Description = c.String(maxLength: 500),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerTypeID = c.Int(nullable: false),
                        CustomerName = c.String(nullable: false, maxLength: 250),
                        CompanyID = c.Int(nullable: false),
                        Address = c.String(maxLength: 500),
                        PhoneNumber = c.String(maxLength: 20),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.CustomerType", t => t.CustomerTypeID, cascadeDelete: true)
                .Index(t => t.CustomerTypeID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.CustomerType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerTypeName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 32),
                        Fullname = c.String(nullable: false, maxLength: 250),
                        CompanyID = c.Int(nullable: false),
                        Image = c.String(maxLength: 500),
                        Age = c.String(),
                        Address = c.String(maxLength: 500),
                        AboutMe = c.String(maxLength: 250),
                        LastOnline = c.DateTime(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.UserAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.TaxValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Value = c.Decimal(precision: 18, scale: 2),
                        CustomerID = c.Int(),
                        InOut = c.Boolean(),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        TaxValue_ID = c.Int(),
                        Type_ID = c.Int(),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TaxValue", t => t.TaxValue_ID)
                .ForeignKey("dbo.Type", t => t.Type_ID)
                .ForeignKey("dbo.Category", t => t.Category_ID)
                .Index(t => t.TaxValue_ID)
                .Index(t => t.Type_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 250),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 250),
                        Description = c.String(maxLength: 250),
                        UnitID = c.Int(nullable: false),
                        UnitID2 = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceDetail", "Unit_ID", "dbo.Unit");
            DropForeignKey("dbo.InvoiceDetail", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Account", "TaxValueID", "dbo.TaxValue");
            DropForeignKey("dbo.Account", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Invoice", "Category_ID", "dbo.Category");
            DropForeignKey("dbo.Invoice", "Type_ID", "dbo.Type");
            DropForeignKey("dbo.Invoice", "TaxValue_ID", "dbo.TaxValue");
            DropForeignKey("dbo.InvoiceDetail", "InvoiceID", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceDetail", "TaxValueID", "dbo.TaxValue");
            DropForeignKey("dbo.InvoiceDetail", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.UserAccount", "UserID", "dbo.User");
            DropForeignKey("dbo.UserAccount", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Customer", "CustomerTypeID", "dbo.CustomerType");
            DropForeignKey("dbo.Customer", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.InvoiceDetail", "CategoryID", "dbo.Category");
            DropIndex("dbo.Invoice", new[] { "Category_ID" });
            DropIndex("dbo.Invoice", new[] { "Type_ID" });
            DropIndex("dbo.Invoice", new[] { "TaxValue_ID" });
            DropIndex("dbo.UserAccount", new[] { "AccountID" });
            DropIndex("dbo.UserAccount", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "CompanyID" });
            DropIndex("dbo.Customer", new[] { "CompanyID" });
            DropIndex("dbo.Customer", new[] { "CustomerTypeID" });
            DropIndex("dbo.Department", new[] { "CompanyID" });
            DropIndex("dbo.InvoiceDetail", new[] { "Unit_ID" });
            DropIndex("dbo.InvoiceDetail", new[] { "Product_ID" });
            DropIndex("dbo.InvoiceDetail", new[] { "TaxValueID" });
            DropIndex("dbo.InvoiceDetail", new[] { "CategoryID" });
            DropIndex("dbo.InvoiceDetail", new[] { "DepartmentID" });
            DropIndex("dbo.InvoiceDetail", new[] { "InvoiceID" });
            DropIndex("dbo.Account", new[] { "TaxValueID" });
            DropIndex("dbo.Account", new[] { "CategoryID" });
            DropTable("dbo.Unit");
            DropTable("dbo.Product");
            DropTable("dbo.Error");
            DropTable("dbo.Type");
            DropTable("dbo.Invoice");
            DropTable("dbo.TaxValue");
            DropTable("dbo.UserAccount");
            DropTable("dbo.User");
            DropTable("dbo.CustomerType");
            DropTable("dbo.Customer");
            DropTable("dbo.Company");
            DropTable("dbo.Department");
            DropTable("dbo.InvoiceDetail");
            DropTable("dbo.Category");
            DropTable("dbo.Account");
        }
    }
}
