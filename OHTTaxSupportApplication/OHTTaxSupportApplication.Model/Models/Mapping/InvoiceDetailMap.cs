using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class InvoiceDetailMap : EntityTypeConfiguration<InvoiceDetail>
    {
        public InvoiceDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("InvoiceDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.InvoiceID).HasColumnName("InvoiceID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.TaxValueID).HasColumnName("TaxValueID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.InvoiceDetails)
                .HasForeignKey(d => d.CategoryID);
            this.HasRequired(t => t.TaxValue)
                .WithMany(t => t.InvoiceDetails)
                .HasForeignKey(d => d.TaxValueID);

        }
    }
}
