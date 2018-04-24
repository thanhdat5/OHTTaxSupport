using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class TaxDetailMap : EntityTypeConfiguration<TaxDetail>
    {
        public TaxDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Note)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("TaxDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TaxID).HasColumnName("TaxID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Quanlity).HasColumnName("Quanlity");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.TaxDetails)
                .HasForeignKey(d => d.ProductID);
            this.HasRequired(t => t.Tax)
                .WithMany(t => t.TaxDetails)
                .HasForeignKey(d => d.TaxID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.TaxDetails)
                .HasForeignKey(d => d.UnitID);

        }
    }
}
