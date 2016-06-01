﻿using System.Data.Entity.ModelConfiguration;
using Gurukul.Business;

namespace Gurukul.DAL.Configurations
{
    public class OfficeAssignmentConfiguration : EntityTypeConfiguration<OfficeAssignment>
    {
        public OfficeAssignmentConfiguration()
        {
            this.HasKey(x => x.InstructorId);

            this.HasRequired(x => x.Instructor).WithOptional(x => x.OfficeAssignment).Map(x=>x.MapKey("OfficeAssignmentInstructorId"));
        }
    }
}