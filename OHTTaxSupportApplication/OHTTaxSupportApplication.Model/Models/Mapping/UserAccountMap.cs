using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class UserAccountMap : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserAccount");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithMany(t => t.UserAccounts)
                .HasForeignKey(d => d.AccountID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserAccounts)
                .HasForeignKey(d => d.UserID);

        }
    }
}
