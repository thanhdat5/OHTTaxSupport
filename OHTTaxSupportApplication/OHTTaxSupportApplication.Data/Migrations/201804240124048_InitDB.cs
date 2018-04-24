namespace OHTTaxSupportApplication.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountCode = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        TaxCategoryID = c.Int(nullable: false),
                        TaxValueID = c.Int(nullable: false),
                        SH = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TaxCategory", t => t.TaxCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.TaxValue", t => t.TaxValueID, cascadeDelete: true)
                .Index(t => t.TaxCategoryID)
                .Index(t => t.TaxValueID);
            
            CreateTable(
                "dbo.TaxCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TaxTypeID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerID = c.Int(nullable: false),
                        TaxValueID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        TaxCategoryID = c.Int(nullable: false),
                        InOut = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.TaxCategory", t => t.TaxCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.TaxType", t => t.TaxTypeID)
                .ForeignKey("dbo.TaxValue", t => t.TaxValueID, cascadeDelete: true)
                .Index(t => t.TaxTypeID)
                .Index(t => t.TaxValueID)
                .Index(t => t.DepartmentID)
                .Index(t => t.TaxCategoryID);
            
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
                        Adderss = c.String(maxLength: 500),
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
                "dbo.TaxDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TaxID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        UnitID = c.Int(nullable: false),
                        Value = c.Decimal(precision: 18, scale: 2),
                        Quanlity = c.Double(nullable: false),
                        Note = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tax", t => t.TaxID, cascadeDelete: true)
                .ForeignKey("dbo.Unit", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.TaxID)
                .Index(t => t.ProductID)
                .Index(t => t.UnitID);
            
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
            
            CreateTable(
                "dbo.TaxType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TaxTypeName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "TaxValueID", "dbo.TaxValue");
            DropForeignKey("dbo.Account", "TaxCategoryID", "dbo.TaxCategory");
            DropForeignKey("dbo.Tax", "TaxValueID", "dbo.TaxValue");
            DropForeignKey("dbo.Tax", "TaxTypeID", "dbo.TaxType");
            DropForeignKey("dbo.TaxDetail", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.TaxDetail", "TaxID", "dbo.Tax");
            DropForeignKey("dbo.TaxDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Tax", "TaxCategoryID", "dbo.TaxCategory");
            DropForeignKey("dbo.Tax", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.UserAccount", "UserID", "dbo.User");
            DropForeignKey("dbo.UserAccount", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Customer", "CustomerTypeID", "dbo.CustomerType");
            DropForeignKey("dbo.Customer", "CompanyID", "dbo.Company");
            DropIndex("dbo.TaxDetail", new[] { "UnitID" });
            DropIndex("dbo.TaxDetail", new[] { "ProductID" });
            DropIndex("dbo.TaxDetail", new[] { "TaxID" });
            DropIndex("dbo.UserAccount", new[] { "AccountID" });
            DropIndex("dbo.UserAccount", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "CompanyID" });
            DropIndex("dbo.Customer", new[] { "CompanyID" });
            DropIndex("dbo.Customer", new[] { "CustomerTypeID" });
            DropIndex("dbo.Department", new[] { "CompanyID" });
            DropIndex("dbo.Tax", new[] { "TaxCategoryID" });
            DropIndex("dbo.Tax", new[] { "DepartmentID" });
            DropIndex("dbo.Tax", new[] { "TaxValueID" });
            DropIndex("dbo.Tax", new[] { "TaxTypeID" });
            DropIndex("dbo.Account", new[] { "TaxValueID" });
            DropIndex("dbo.Account", new[] { "TaxCategoryID" });
            DropTable("dbo.Error");
            DropTable("dbo.TaxValue");
            DropTable("dbo.TaxType");
            DropTable("dbo.Unit");
            DropTable("dbo.Product");
            DropTable("dbo.TaxDetail");
            DropTable("dbo.UserAccount");
            DropTable("dbo.User");
            DropTable("dbo.CustomerType");
            DropTable("dbo.Customer");
            DropTable("dbo.Company");
            DropTable("dbo.Department");
            DropTable("dbo.Tax");
            DropTable("dbo.TaxCategory");
            DropTable("dbo.Account");
        }
    }
}
