using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TaxValueMap : EntityTypeConfiguration<TaxValue>
    {
        public TaxValueMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TaxValue");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
