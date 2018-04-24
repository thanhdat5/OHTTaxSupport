using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.AccountCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SH)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AccountCode).HasColumnName("AccountCode");
            this.Property(t => t.TaxCategoryID).HasColumnName("TaxCategoryID");
            this.Property(t => t.TaxValueID).HasColumnName("TaxValueID");
            this.Property(t => t.SH).HasColumnName("SH");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.TaxCategory)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.TaxCategoryID);
            this.HasRequired(t => t.TaxValue)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.TaxValueID);

        }
    }
}
