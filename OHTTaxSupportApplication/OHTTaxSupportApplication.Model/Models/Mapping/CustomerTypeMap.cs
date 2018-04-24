using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class CustomerTypeMap : EntityTypeConfiguration<CustomerType>
    {
        public CustomerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CustomerTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CustomerType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CustomerTypeName).HasColumnName("CustomerTypeName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
