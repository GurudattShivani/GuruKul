using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gurukul.Business
{
    public class Department : IModel
    {
        public int Id { get; set; }
        public int? AdministratorId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public Instructor Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }

        public byte[] RowVersion { get; set; }

    }
}