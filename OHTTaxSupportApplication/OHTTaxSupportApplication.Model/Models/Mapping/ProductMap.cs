using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Description)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.UnitID2).HasColumnName("UnitID2");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
                                      

        }
    }
}
