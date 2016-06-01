using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Gurukul.Business;

namespace Gurukul.DAL
{
    public class GurukulDataInitializer : DropCreateDatabaseIfModelChanges<GurukulDbContext>
    {
        protected override void Seed(GurukulDbContext context)
        {
            SeedStudents(context);
            SeedCourses(context);
            SeedEnrollments(context);
        }

        private void SeedStudents(GurukulDbContext context)
        {
            context.Students.AddOrUpdate(x => x.FirstName,
                new Student {FirstName = "Gurudatt", LastName = "Satyanarayan", EnrolledDate = DateTime.Now},
                new Student { FirstName = "Poornima", LastName = "Ravishankar", EnrolledDate = DateTime.Now },
                new Student { FirstName = "Van", LastName = "Nguyen", EnrolledDate = DateTime.Now }
                );

            context.SaveChanges();
        }

        private void SeedCourses(GurukulDbContext context)
        {
            context.Courses.AddOrUpdate(x => x.Title,
                new Course {Title = "C#", Credits = 5},
                new Course {Title = "Web Services", Credits = 4},
                new Course {Title = "Javascript", Credits = 3},
                new Course {Title = "Databases", Credits = 5},
                new Course {Title = "Professional Development", Credits = 3}
                );

            context.SaveChanges();
        }

        private void SeedEnrollments(GurukulDbContext context)
        {
            context.Enrollments.AddOrUpdate(
                new Enrollment
                {
                    Student = context.Students.FirstOrDefault(x => x.FirstName == "Gurudatt"),
                    Course = context.Courses.FirstOrDefault(x => x.Title == "C#"),
                    Grade = Grade.A
                },
                new Enrollment
                {
                    Student = context.Students.FirstOrDefault(x => x.FirstName == "Poornima"),
                    Course = context.Courses.FirstOrDefault(x => x.Title == "Databases"),
                    Grade = Grade.A
                },
                new Enrollment
                {
                    Student = context.Students.FirstOrDefault(x => x.FirstName == "van"),
                    Course = context.Courses.FirstOrDefault(x => x.Title == "Web Services"),
                    Grade = Grade.A
                }
                );

            context.SaveChanges();
        }

    }
}