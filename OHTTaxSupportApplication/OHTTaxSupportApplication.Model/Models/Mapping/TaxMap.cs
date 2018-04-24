using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TaxMap : EntityTypeConfiguration<Tax>
    {
        public TaxMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Tax");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TaxTypeID).HasColumnName("TaxTypeID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TaxValueID).HasColumnName("TaxValueID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.TaxCategoryID).HasColumnName("TaxCategoryID");
            this.Property(t => t.InOut).HasColumnName("InOut");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.DepartmentID);
            this.HasRequired(t => t.TaxCategory)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.TaxCategoryID);
            this.HasOptional(t => t.TaxType)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.TaxTypeID);
            this.HasRequired(t => t.TaxValue)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.TaxValueID);

        }
    }
}
