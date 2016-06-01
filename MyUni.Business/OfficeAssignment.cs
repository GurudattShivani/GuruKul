using System.ComponentModel.DataAnnotations;

namespace Gurukul.Business
{
    public class OfficeAssignment
    {
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [Display(Name = "Office Location")]
        public string Location { get; set; }
    }
}