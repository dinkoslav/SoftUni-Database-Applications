using System.Linq;

namespace EntityFramework
{
    using System;
    using System.Diagnostics;
    class EntityFramework
    {
        static void Main(string[] args)
        {
            //
            // Problem 2.Employee DAO Class
            //

            var employee = new Employee()
            {
                FirstName = "Pencho",
                LastName = "Minkov",
                JobTitle = "CC&B Developer",
                DepartmentID = 1,
                HireDate = DateTime.Now,
                Salary = 1500
            };

            //DAO.Add(employee);

            //Employee employee1 = DAO.FindByKey(294);
            //Console.WriteLine(employee1.FirstName + " " + employee1.LastName);

            //employee1.FirstName = "Angel";

            //DAO.Modify(employee1);

            //DAO.Delete(employee);

            //
            // Problem 3.Database Search Queries
            //

            var db = new SoftUniEntities();

            // 1.Find all employees who have projects started in the time period 2001 - 2003 (inclusive). Select each employee's first name, last name and manager name and each of their projects' name, start date, end date.

            //using (db)
            //{
            //    var employees = db.Employees
            //    .Where(emp => emp.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
            //    .Select(emp => new
            //    {
            //        FirstName = emp.FirstName,
            //        LastName = emp.LastName,
            //        Projects = emp.Projects
            //        .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
            //        .Select(p => p.Name + ", start date: " + p.StartDate),
            //        Manager = emp.Employee1.FirstName + " " + emp.Employee1.LastName
            //    });

            //    foreach (var empl in employees)
            //    {
            //        Console.Write(
            //            empl.FirstName + " " 
            //            + empl.LastName + ", Manager - "
            //            + empl.Manager + ", Projects - ");
            //        foreach (var project in empl.Projects)
            //        {
            //            Console.Write("{0} ", project.ToString());
            //        }
            //        Console.WriteLine();
            //    }

            //}

            // 2.Find all addresses, ordered by the number of employees who live there (descending), then by town name (ascending). Take only the first 10 addresses and select their address text, town name and employee count. 

            //using (db)
            //{
            //    var addresess =
            //        (from a in db.Addresses
            //        orderby a.Employees.Count descending, a.Town.Name ascending
                    
            //        select new
            //        {
            //            Adress = a.AddressText,
            //            Town = a.Town.Name,
            //            EmployeeCount = a.Employees.Count
            //        }).Take(10);

            //    foreach (var address in addresess)
            //    {
            //        Console.WriteLine("{0}, {1} - {2} emplayees",
            //            address.Adress,
            //            address.Town,
            //            address.EmployeeCount);
            //    }
            //}

            // 3.Get an employee by id (e.g. 147). Select only his/her first name, last name, job title and projects (only their names). The projects should be ordered by name (ascending).

            //using (db)
            //{

            //    var employeeById = db.Employees
            //            .Where(empl => empl.EmployeeID == 147)
            //            .Select(empl => new
            //            {
            //                Info = empl.FirstName + " " + empl.LastName + ", Job Title: " + empl.JobTitle + ", Projects: ",
            //                Projects = empl.Projects
            //                    .OrderBy(p => p.Name)
            //                    .Select(p => p.Name)
            //            }
            //            ).FirstOrDefault();

            //    Console.Write(employeeById.Info);
            //    foreach (var project in employeeById.Projects)
            //    {
            //        Console.Write(project.ToString() + ", ");
            //    }
            //    Console.WriteLine();
            //}

            //4.Find all departments with more than 5 employees. Order them by employee count (ascending). Select the department name, manager name and first name, last name, hire date and job title of every employee.

            //using (db)
            //{
            //    var departments = db.Departments
            //        .Where(d => d.Employees.Count > 5)
            //        .OrderBy(d => d.Employees.Count)
            //        .Select(d => new
            //        {
            //            DepartmentName = d.Name,
            //            Manager = d.Employee.FirstName,
            //            Employees = d.Employees
            //                .Select(e => new
            //                {
            //                    Info = e.FirstName + " " + e.LastName + " " + e.HireDate + " " + e.JobTitle
            //                })
            //        });

            //    foreach (var department in departments)
            //    {
            //        Console.WriteLine("{0}: {1} - ", department.DepartmentName, department.Manager);
            //        foreach (var empl in department.Employees)
            //        {
            //            Console.WriteLine("  {0}", empl.Info);
            //        }
            //    }
            //}

            //
            // Problem 4.Native SQL Query
            //

            //var totalcount = db.Employees.Count();

            //var sw = new Stopwatch();
            //sw.Start();
            //PrintNamesWithNativeQuery();
            //Console.WriteLine("Native: {0}", sw.Elapsed);
            
            //sw.Restart();
            //PrintNamesWithLinqQuery();
            //Console.WriteLine("Linq: {0}", sw.Elapsed);

            //
            // Problem 5.Concurrent Database Changes with EF
            //

            //var employee1 = db.Employees.Find(294);
            //employee1.FirstName = "Angel";

            //var db2 = new SoftUniEntities();
            //var employee2 = db2.Employees.Find(294);
            //employee1.FirstName = "Acho";

            //db.SaveChanges();
            //db2.SaveChanges();

            //
            // Problem 6.Call a Stored Procedure
            //

            // Stored Procedure - SQL

            //CREATE PROCEDURE GetProjectsByEmployee
            //    @FirstName nvarchar(50), 
            //    @LastName nvarchar(50) 
            //AS 
            //    SET NOCOUNT ON;
            //    SELECT p.Name, p.Description, p.StartDate
            //    FROM dbo.Employees AS e
            //    JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
            //    JOIN Projects AS p ON ep.ProjectID = p.ProjectID
            //    WHERE e.FirstName = @FirstName AND e.LastName = @LastName
            //GO

            var projects = db.GetProjectsByEmployee("Rob", "Walters");
            foreach (var project in projects)
            {
                Console.WriteLine(project.Name + ", " + project.Description + ", " + project.StartDate);
            }

        }

        private static void PrintNamesWithLinqQuery()
        {
            var db = new SoftUniEntities();
            using (db)
            {
                var employees = db.Employees
                    .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                    .Select(e => e.FirstName);
            }
        }

        private static void PrintNamesWithNativeQuery()
        {
            var db = new SoftUniEntities();
            string nativeSQLQuery =
                "SELECT e.FirstName, p.StartDate" +
                "FROM dbo.Employees AS e" +
                "JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID" +
                "JOIN Projects AS p ON ep.ProjectID = p.ProjectID" +
                "WHERE p.StartDate BETWEEN '2002-01-01' AND '2002-12-31'";

            var employees = db.Database.SqlQuery<string>(
                nativeSQLQuery, "Employees");
        }
    }
}
