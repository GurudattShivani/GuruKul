using System.Data.Entity.ModelConfiguration;
using Gurukul.Business;

namespace Gurukul.DAL.Configurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            this.HasRequired(x => x.Department).WithMany(x => x.Courses).HasForeignKey(x => x.DepartmentId);
        }
    }
}