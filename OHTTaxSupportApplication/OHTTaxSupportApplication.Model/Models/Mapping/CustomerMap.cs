using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Adderss)
                .HasMaxLength(500);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Adderss).HasColumnName("Adderss");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.CompanyID);
            this.HasRequired(t => t.CustomerType)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.CustomerTypeID);

        }
    }
}
