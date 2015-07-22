namespace StudentSystem.ConsoleClient
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    class ConsoleClient
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();

            //1.Lists all students and their homework submissions. Select only their names and for each homework - content and content-type.

            //var studentsHomeworks = context.Students
            //    .Select(s => new
            //    {
            //        Name = s.Name,
            //        Homeworks = s.Courses.Select(c => c.Homeworks.Select(
            //            h => new
            //            {
            //                Content = "  ContentType: " + h.ContentType + ", content: " + h.Content
            //            })
            //            )
            //    });

            //foreach (var studentsHomework in studentsHomeworks)
            //{
            //    Console.WriteLine("Student " + studentsHomework.Name + "have submissions on: ");
            //    foreach (var homework in studentsHomework.Homeworks)
            //    {
            //        foreach (var hw in homework)
            //        {
            //            Console.WriteLine(hw.Content);
            //        }
            //    }
            //}

            //2.List all courses with their corresponding resources. Select the course name and description and everything for each resource. Order the courses by start date (ascending), then by end date (descending).

            //var courses = context.Courses
            //    .OrderBy(c => c.StartDate)
            //    .ThenByDescending(c => c.EndDate)
            //    .Select(c => new
            //{
            //    Name = c.Name,
            //    Description = c.Description,
            //    Resources = c.Resources.Select(r => new
            //    {
            //        Name = r.Name,
            //        Type = r.Type,
            //        Url = r.Url
            //    })
            //});

            //foreach (var course in courses)
            //{
            //    Console.WriteLine("{0}: {1} have as resource:", course.Name, course.Description);
            //    foreach (var resource in course.Resources)
            //    {
            //        Console.WriteLine("  {0} is of type {1} and the url is {2}", resource.Name, resource.Type, resource.Url);
            //    }
            //}

            //3.List all courses with more than 5 resources. Order them by resources count (descending), then by start date (descending). Select only the course name and the resource count.

            //var courses2 = context.Courses
            //    .OrderByDescending(c => c.Resources.Count)
            //    .ThenByDescending(c => c.StartDate)
            //    .Where(c => c.Resources.Count >= 5)
            //    .Select(c => new
            //{
            //    Name = c.Name,
            //    ResourceCount = c.Resources.Count
            //});

            //foreach (var course in courses2)
            //{
            //    Console.WriteLine("{0} have {1} resources", course.Name, course.ResourceCount);
            //}

            //4.List all courses which were active on a given date (choose the date depending on the data seeded to ensure there are results), and for each course count the number of students enrolled. Select the course name, start and end date, course duration (difference between end and start date) and number of students enrolled. Order the results by the number of students enrolled (in descending order), then by duration (descending).

            //var courses3 = context.Courses
            //    .OrderByDescending(c => c.Students.Count)
            //    .ThenByDescending(c => DbFunctions.DiffHours(c.StartDate, c.EndDate))
            //    .Where(c => c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now)
            //    .Select(c => new
            //{
            //    Name = c.Name,
            //    StartDate = c.StartDate,
            //    EndDate = c.EndDate,
            //    Duration = DbFunctions.DiffHours(
            //         c.StartDate, c.EndDate),
            //    StudentsCount = c.Students.Count
            //});

            //foreach (var course in courses3)
            //{
            //    Console.WriteLine("{0} started {1} and ends {2}. The duration is {4} hours and the students enrolled are {3}",
            //        course.Name, course.StartDate, course.EndDate, course.StudentsCount, course.Duration);
            //}

            //5.For each student, calculate the number of courses she’s enrolled in, the total price of these courses and the average price per course for the student. Select the student name, number of courses, total price and average price. Order the results by total price (descending), then by number of courses (descending) and then by the student’s name (ascending).

            var students = context.Students
                .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                .ThenByDescending(s => s.Courses.Count)
                .ThenBy(s => s.Name)
                .Select(s => new
                {
                    Name = s.Name,
                    CoursesCount = (int?)s.Courses.Count,
                    CoursesSum = (decimal?)s.Courses.Sum(c => c.Price),
                    CoursesAverage = (decimal?)s.Courses.Average(c => c.Price)
                });

            foreach (var student in students)
            {
                Console.WriteLine("{0} has enrolled in {1} courses. The total price for the courses is {2} and the average price per course is {3}", student.Name, student.CoursesCount, student.CoursesSum, student.CoursesAverage);
            }
        }
    }
}
