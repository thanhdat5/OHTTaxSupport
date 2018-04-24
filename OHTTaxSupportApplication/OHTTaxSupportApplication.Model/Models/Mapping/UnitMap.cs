using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Unit");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
