using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Invoice");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.InOut).HasColumnName("InOut");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships

        }
    }
}
