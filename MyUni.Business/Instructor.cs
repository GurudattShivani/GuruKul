using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gurukul.Business
{
    public class Instructor : IModel
    {
        public int Id { get; set; }
        public int? OfficeAssignmentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }

        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public ICollection<Course> Courses { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }

        public ICollection<Department> DepartmentsWhichAdministers { get; set; }
    }
}