using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TaxCategoryMap : EntityTypeConfiguration<TaxCategory>
    {
        public TaxCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("TaxCategory");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
