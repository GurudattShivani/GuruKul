using System.Data.Entity.ModelConfiguration;
using Gurukul.Business;

namespace Gurukul.DAL.Configurations
{
    public class InstructorConfiguration : EntityTypeConfiguration<Instructor>
    {
        public InstructorConfiguration()
        {
            this.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            this.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            this.Ignore(x => x.FullName);

            this.HasMany(x => x.Courses).WithMany(x => x.Instructors);
            
            this.Property(x => x.HireDate).HasColumnType("datetime2");
            

        }
    }
}