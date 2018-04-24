using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OHTTaxSupportApplication.Model.Models.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DepartmentName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Address)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Department");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Departments)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
