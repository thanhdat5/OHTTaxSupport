using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Fullname)
                .IsRequired()
                .HasMaxLength(250);
            this.Property(t => t.Image)
                .HasMaxLength(500);

            this.Property(t => t.Address)
                .HasMaxLength(500);

            this.Property(t => t.AboutMe)
                .HasMaxLength(250);

            this.Property(t => t.Image)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.AboutMe).HasColumnName("AboutMe");
            this.Property(t => t.LastOnline).HasColumnName("LastOnline");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
