using Microsoft.EntityFrameworkCore;
using NewEF.Entities;

namespace NewEF
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();

            db.Database.EnsureCreated();

            if (!db.Departments.Any())
            {
                CreateSampleData(db);
            }

            ReadDepartments(db);
            ReadTeachers(db);
            ReadStudents(db);
            ReadPassports(db);
        }
        static void CreateSampleData(AppDbContext db)
        {
            var dep = new Department
            {
                Name = "Computer Science",
                Description = "Department of Computer Science",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var algo = new Subject
            {
                Name = "Algorithms",
                Description = "Intro to algorithms",
                Department = dep,
                Teachers = new List<Teacher>()
            };

            var teacher = new Teacher
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                BirthDate = new DateTime(1985, 5, 20),
                Salary = 35000m,
                Department = dep,
                Subjects = new List<Subject>(),
                Groups = new List<Group>()
            };

            dep.Subjects.Add(algo);
            dep.Teachers.Add(teacher);
            teacher.Subjects.Add(algo);
            algo.Teachers.Add(teacher);

            db.Departments.Add(dep);
            db.Teachers.Add(teacher);
            db.Subjects.Add(algo);

            db.SaveChanges();

            var group = new Group
            {
                Name = "CS-101",
                Students = new List<Student>()
            };
            db.Groups.Add(group);
            db.SaveChanges();

            var student = new Student
            {
                FirstName = "Petro",
                LastName = "Petrenko",
                Email = "petro.petrenko@example.com",
                Birthday = new DateTime(2005, 3, 1),
                Scholarship = 120.50m,
                GroupId = group.Id,   // або Group = group
                _attendanceForm = Student.AttendanceForm.Offline,
            };
            db.Students.Add(student);
            db.SaveChanges();

            var passport = new Passport
            {
                Name = $"{student.FirstName} {student.LastName}",
                Number = "123456789",
                StudentId = student.Id
            };
            db.Passports.Add(passport);
            db.SaveChanges();

            student.PassportId = passport.Id;
            db.Students.Update(student);
            db.SaveChanges();

            Console.WriteLine("Sample data created.");
        }

        static void ReadDepartments(AppDbContext db)
        {
            Console.WriteLine("=== Departments (with teachers and subjects) ===");
            var deps = db.Departments
                         .Include(d => d.Teachers)
                         .Include(d => d.Subjects)
                         .ToList();

            foreach (var d in deps)
            {
                Console.WriteLine(d.ToString());
            }
        }

        static void ReadTeachers(AppDbContext db)
        {
            Console.WriteLine("=== Teachers (with subjects and groups) ===");
            var teachers = db.Teachers
                             .Include(t => t.Subjects)
                             .Include(t => t.Groups)
                             .Include(t => t.Department)
                             .ToList();

            foreach (var t in teachers)
            {
                Console.WriteLine(t.ToString());
            }
        }

        static void ReadStudents(AppDbContext db)
        {
            Console.WriteLine("=== Students (with group and passport) ===");
            var students = db.Students
                             .Include(s => s.Group)
                             .Include(s => s.Passport)
                             .ToList();

            foreach (var s in students)
            {
                Console.WriteLine(s.ToString());
                if (s.Passport != null)
                    Console.WriteLine($"  Passport: {s.Passport.Id} | {s.Passport.Number}");
            }
        }

        static void ReadPassports(AppDbContext db)
        {
            Console.WriteLine("=== Passports (with student) ===");
            var passports = db.Passports
                              .Include(p => p.Student)
                              .ToList();

            foreach (var p in passports)
            {
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Number} | Student: {p.Student?.FirstName} {p.Student?.LastName}");
            }
        }
    }
}
