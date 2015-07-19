using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EntityFramework
{
    public static class DAO
    {
        private static SoftUniEntities db; 

        static DAO()
        {
            db = new SoftUniEntities();
        }

        public static void Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public static Employee FindByKey(object key)
        {
            var employee = db.Employees.Find(key);
            return employee;
        }

        public static void Modify(Employee employee)
        {
            using (db)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Delete(Employee employee)
        {
            using (db)
            {
                var employee1 = db.Employees
                    .FirstOrDefault(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName);
                db.Employees.Remove(employee1);
                db.SaveChanges();
            }
        }
    }
}
