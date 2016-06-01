using System.Data.Entity.ModelConfiguration;
using Gurukul.Business;

namespace Gurukul.DAL.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            this.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FirstName");

            this.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Ignore(x => x.FullName);

            this.Property(x => x.EnrolledDate).HasColumnType("datetime2");
        }
    }
}