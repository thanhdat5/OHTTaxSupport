using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TypeMap : EntityTypeConfiguration<Type>
    {
        public TypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Type");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
