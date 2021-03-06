using System.Data.Entity;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.Models.Mapping;

namespace OHTTaxSupportApplication.Data
{
    public partial class OHTTaxSupport_dbContext : DbContext
    {
        static OHTTaxSupport_dbContext()
        {
            Database.SetInitializer<OHTTaxSupport_dbContext>(null);
        }

        public OHTTaxSupport_dbContext()
            : base("Name=OHTTaxSupport_dbContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Product> Products { get; set; }   
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<TaxValue> TaxValues { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerTypeMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ErrorMap());
            modelBuilder.Configurations.Add(new ProductMap());   
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new InvoiceDetailMap());
            modelBuilder.Configurations.Add(new TypeMap());
            modelBuilder.Configurations.Add(new TaxValueMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserAccountMap());
        }
    }
}
