namespace StudentSystem.Data.Migrations
{
    using _2.Entity_Framework_Code_First;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemContext";
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            for (int i = 0; i < 50; i++)
            {
                context.Students.AddOrUpdate(
                    s => s.Name,
                    new Student()
                    {
                        Name = "Student " + i,
                        PhoneNumber = "08779911" + i,
                        Birthday = DateTime.Now,
                        RegistrationDate = DateTime.Now
                    });
            }

            context.SaveChanges();

            for (int i = 0; i < 50; i++)
            {
                context.Courses.AddOrUpdate(
                    c => c.Name,
                    new Course
                    {
                        Name = "Course " + i,
                        Description = "Seed data for course " + i,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(i),
                        Price = (1000 + i) * i,
                        Students = new List<Student>()
                        {
                            context.Students.Find(i),
                            context.Students.Find(i % 2),
                            context.Students.Find(i % 5)
                        },
                        Resources = new List<Resource>()
                        {
                            new Resource()
                            {
                                Name = "Resource 1 for Course " + i,
                                Type = ResourceType.Document,
                                Url = "http://res1" + i + ".com"
                            },
                            new Resource()
                            {
                                Name = "Resource 2 for Course " + i,
                                Type = ResourceType.Document,
                                Url = "http://res2" + i + ".com"
                            },
                            new Resource()
                            {
                                Name = "Resource 3 for Course " + i,
                                Type = ResourceType.Document,
                                Url = "http://res3" + i + ".com"
                            },
                            new Resource()
                            {
                                Name = "Resource 4 for Course " + i,
                                Type = ResourceType.Document,
                                Url = "http://res4" + i + ".com"
                            },
                            new Resource()
                            {
                                Name = "Resource 5 for Course " + i,
                                Type = ResourceType.Document,
                                Url = "http://res5" + i + ".com"
                            }
                        },
                        Homeworks = new List<Homework>()
                        {
                            new Homework()
                            {
                                Content = "Content 1 " + i,
                                StudentId = 1,
                                SubmissionDate = DateTime.Now,
                                ContentType = ContentType.Application
                            },
                            new Homework()
                            {
                                Content = "Content 2 " + i,
                                StudentId = 10,
                                SubmissionDate = DateTime.Now,
                                ContentType = ContentType.Application
                            },
                            new Homework()
                            {
                                Content = "Content 3 " + i,
                                StudentId = 20,
                                SubmissionDate = DateTime.Now,
                                ContentType = ContentType.Application
                            }
                        }
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
