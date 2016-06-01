using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gurukul.Business;

namespace Gurukul.Web.ViewModels
{
    public class InstructorListViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}