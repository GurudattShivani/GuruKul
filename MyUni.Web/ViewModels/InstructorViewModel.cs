using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Gurukul.Web.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string Location { get; set; }

        public List<CourseViewModel> Courses { get; set; }
        
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}