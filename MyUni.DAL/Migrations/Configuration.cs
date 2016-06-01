using System.Collections.ObjectModel;
using System.Diagnostics;
using Gurukul.Business;

namespace Gurukul.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GurukulDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GurukulDbContext context)
        {
            try
            {
                SeedInstructors(context);
                SeedOfficeAssignments(context);
                SeedDepartments(context);
                SeedStudents(context);
                SeedCourses(context);
                SeedEnrollments(context);
            }
            catch (Exception exception)
            {
                
            }
        }

        private void SeedStudents(GurukulDbContext context)
        {
            context.Students.AddOrUpdate(x => x.FirstName,
                new Student { FirstName = "Gurudatt", LastName = "Satyanarayan", EnrolledDate = DateTime.Now },
                new Student { FirstName = "Poornima", LastName = "Ravishankar", EnrolledDate = DateTime.Now },
                new Student { FirstName = "Van", LastName = "Nguyen", EnrolledDate = DateTime.Now }
                );

            context.SaveChanges();
        }

        private void SeedInstructors(GurukulDbContext context)
        {
            context.Instructors.AddOrUpdate(x=>x.FirstName,
                new Instructor { FirstName = "Bill", LastName = "Gates", HireDate = DateTime.Now.AddYears(-50)},
                new Instructor { FirstName = "Mr. Career", LastName = "Agnostic", HireDate = DateTime.Now.AddYears(-50) }
                );

            context.SaveChanges();
        }

        private void SeedOfficeAssignments(GurukulDbContext context)
        {
            context.OfficeAssignments.AddOrUpdate(x=>x.Location,
                new OfficeAssignment { Location = "Silicon Valley", Instructor = context.Instructors.FirstOrDefault(x=>x.LastName == "Gates")},
                new OfficeAssignment { Location = "Google", Instructor = context.Instructors.FirstOrDefault(x => x.FirstName == "Mr. Career") }
                );

            context.SaveChanges();
        }

        private void SeedDepartments(GurukulDbContext context)
        {
            context.Departments.AddOrUpdate(x=>x.Name,
                new Department { Name = "Microsoft",Budget = 5000000, Administrator = context.Instructors.FirstOrDefault(x=>x.LastName == "Gates"),StartDate = DateTime.Now.AddYears(-40), RowVersion = Guid.NewGuid().ToByteArray()},
                new Department { Name = "Career Development", Budget = 1000000, Administrator = context.Instructors.FirstOrDefault(x => x.FirstName == "Mr. Career"), StartDate = DateTime.Now.AddYears(-40), RowVersion = Guid.NewGuid().ToByteArray() },
                new Department { Name = "Client Technologies", Budget = 1000000, RowVersion = Guid.NewGuid().ToByteArray() },
                new Department { Name = "Databases", Budget = 1000000, RowVersion = Guid.NewGuid().ToByteArray() },
                new Department { Name = "Web Services", Budget = 1000000, RowVersion = Guid.NewGuid().ToByteArray() }
                );

            context.SaveChanges();
        }

        private void SeedCourses(GurukulDbContext context)
        {

            var microsoft = context.Departments.FirstOrDefault(x => x.Name == "Microsoft");
            var clientTech = context.Departments.FirstOrDefault(x => x.Name == "Client Technologies");
            var databases = context.Departments.FirstOrDefault(x => x.Name == "Databases");
            var career = context.Departments.FirstOrDefault(x => x.Name == "Career Development");

            var bill = context.Instructors.FirstOrDefault(x => x.FirstName == "Bill");
            var mrCareer = context.Instructors.FirstOrDefault(x => x.FirstName == "Mr. Career");

            context.Courses.AddOrUpdate(x => x.Title,
                new Course { Title = "C#", Credits = 5, DepartmentId = microsoft.Id, Instructors = new Collection<Instructor>(new []{bill})},
                new Course { Title = "ASP.NET Web Api", Credits = 4, DepartmentId = microsoft.Id, Instructors = new Collection<Instructor>(new[] { bill }) },
                new Course { Title = "Javascript", Credits = 3, DepartmentId = clientTech.Id },
                new Course { Title = "MS SQL Server", Credits = 5, DepartmentId = databases.Id, Instructors = new Collection<Instructor>(new[] { bill }) },
                new Course { Title = "Professional Development", Credits = 3, DepartmentId = career.Id, Instructors = new Collection<Instructor>(new[] { mrCareer }) }
                );

            context.SaveChanges();
        }

        private void SeedEnrollments(GurukulDbContext context)
        {
            //
            // Students
            //
            var gurudatt = context.Students.FirstOrDefault(x => x.FirstName == "gurudatt");
            var poornima = context.Students.FirstOrDefault(x => x.FirstName == "poornima");
            var van = context.Students.FirstOrDefault(x => x.FirstName == "van");
            //
            // Courses
            //
            var cSharp = context.Courses.FirstOrDefault(x => x.Title == "C#");
            var msSqlServer = context.Courses.FirstOrDefault(x => x.Title == "MS SQL Server");
            var webApi = context.Courses.FirstOrDefault(x => x.Title == "ASP.NET Web Api");

            context.Enrollments.AddOrUpdate(x=>new{x.StudentId, x.CourseId},
                new Enrollment { StudentId = gurudatt.Id, CourseId = cSharp.Id, Grade = Grade.A},
                new Enrollment { StudentId = poornima.Id, CourseId = msSqlServer.Id, Grade = Grade.A },
                new Enrollment { StudentId = van.Id, CourseId = webApi.Id, Grade = Grade.A }
                );          

            context.SaveChanges();
        }
    }
}
