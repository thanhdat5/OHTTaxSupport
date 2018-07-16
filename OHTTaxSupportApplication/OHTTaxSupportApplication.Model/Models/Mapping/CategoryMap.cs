using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
