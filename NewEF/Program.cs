using Microsoft.EntityFrameworkCore;
using NewEF.Entities;

namespace NewEF
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create
            using var db = new AppDbContext();

            var student = new Student
            {
                FirstName = "Ivan",
                LastName = "Petrenko",
                Email = "ivan@gmail.com",
                Scholarship = 1500,
                GroupId = 1
            };

            db.Students.Add(student);
            db.SaveChanges();

            // Read
            var students = db.Students
                .Include(s => s.Group)
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName}");
            }

            db.SaveChange
        }
    }
}
