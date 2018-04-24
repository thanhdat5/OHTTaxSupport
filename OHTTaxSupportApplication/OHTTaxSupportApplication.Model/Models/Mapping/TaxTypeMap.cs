using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TaxTypeMap : EntityTypeConfiguration<TaxType>
    {
        public TaxTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TaxTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TaxType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TaxTypeName).HasColumnName("TaxTypeName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
