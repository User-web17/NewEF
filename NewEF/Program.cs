using Microsoft.EntityFrameworkCore;
using NewEF.Entities;

namespace NewEF
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();

            //InitializeDB(db);

            foreach (var student in db.StudentGroupViews)
            {
                Console.WriteLine($"{student.StudentName} | {student.GroupName}");
            }

            foreach (var student in db.TeacherSubjectViews)
            {
                Console.WriteLine($"{student.TeacherName} | {student.SubjectName}");
            }
        }

        static void InitializeDB(AppDbContext db)
        {
            if (db.Departments.Any())
            {
                Console.WriteLine("Database already initialized.");
                return;
            }

            var itDepartment = new Department
            {
                Name = "IT Department",
                Description = "Programming and Software Engineering",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var mathDepartment = new Department
            {
                Name = "Mathematics Department",
                Description = "Pure and Applied Mathematics",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var linguistDepartment = new Department
            {
                Name = "Linguist Department",
                Description = "Linguistics",
                Teachers = new List<Teacher>(),
                Subjects = new List<Subject>()
            };

            var csharp = new Subject
            {
                Name = "C#",
                Description = ".NET and WinForms",
                Department = itDepartment,
                Teachers = new List<Teacher>()
            };

            var databases = new Subject
            {
                Name = "Databases",
                Description = "SQL Server and EF Core",
                Department = itDepartment,
                Teachers = new List<Teacher>()
            };

            var algebra = new Subject
            {
                Name = "Algebra",
                Description = "Linear Algebra",
                Department = mathDepartment,
                Teachers = new List<Teacher>()
            };

            var ukrainian = new Subject
            {
                Name = "Ukrainian",
                Description = "In-depth Ukrainian",
                Department = linguistDepartment,
                Teachers = new List<Teacher>()
            };

            var english = new Subject
            {
                Name = "English",
                Description = "In-depth English",
                Department = linguistDepartment,
                Teachers = new List<Teacher>()
            };

            var groupA = new Group
            {
                Name = "IT-101",
                Students = new List<Student>(),
            };

            var groupB = new Group
            {
                Name = "MATH-202",
                Students = new List<Student>(),
            };

            var groupC = new Group
            {
                Name = "DB-303",
                Students = new List<Student>(),
            };

            var groupD = new Group
            {
                Name = "UKR-404",
                Students = new List<Student>(),
            };

            var groupE = new Group
            {
                Name = "UKR-505",
                Students = new List<Student>(),
            };

            var teacher1 = new Teacher
            {
                FirstName = "John",
                LastName = "Smith",
                BirthDate = new DateTime(1985, 4, 10),
                Salary = 3000,
                Department = itDepartment,
                Subjects = new List<Subject> { csharp, databases },
                Groups = new List<Group> { groupA }
            };

            var teacher2 = new Teacher
            {
                FirstName = "Anna",
                LastName = "Myhailivna",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = mathDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var teacher3 = new Teacher
            {
                FirstName = "Michael",
                LastName = "Clear",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = linguistDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var teacher4 = new Teacher
            {
                FirstName = "May",
                LastName = "Brown",
                BirthDate = new DateTime(1990, 6, 22),
                Salary = 2800,
                Department = linguistDepartment,
                Subjects = new List<Subject> { algebra },
                Groups = new List<Group> { groupB }
            };

            var student1 = new Student
            {
                FirstName = "Alex",
                LastName = "Shevchenko",
                Email = "alex@gmail.com",
                Birthdate = new DateTime(2004, 2, 15),
                Group = groupA
            };

            var student2 = new Student
            {
                FirstName = "Maria",
                LastName = "Hvylova",
                Email = "maria@gmail.com",
                Birthdate = new DateTime(2005, 8, 5),
                Group = groupA
            };

            var student3 = new Student
            {
                FirstName = "David",
                LastName = "Kozak",
                Email = "david@gmail.com",
                Birthdate = new DateTime(2003, 11, 20),
                Group = groupB
            };

            var student4 = new Student
            {
                FirstName = "Sofia",
                LastName = "Zvychaina",
                Email = "sofia@gmail.com",
                Birthdate = new DateTime(2004, 2, 15),
                Group = groupC
            };

            var student5 = new Student
            {
                FirstName = "Kyrylo",
                LastName = "Mefodiyovich",
                Email = "kyrylo@gmail.com",
                Birthdate = new DateTime(2005, 8, 5),
                Group = groupD
            };

            var student6 = new Student
            {
                FirstName = "Myhailo",
                LastName = "Melnyk",
                Email = "myhailo@gmail.com",
                Birthdate = new DateTime(2003, 11, 20),
                Group = groupE
            };

            db.Departments.AddRange(itDepartment, mathDepartment, linguistDepartment);
            db.Subjects.AddRange(csharp, databases, algebra, ukrainian, english);
            db.Groups.AddRange(groupA, groupB, groupC, groupD, groupE);
            db.Teachers.AddRange(teacher1, teacher2, teacher3, teacher4);
            db.Students.AddRange(student1, student2, student3, student4, student5, student6);

            db.SaveChanges();

            Console.WriteLine("Database initialized successfully.");
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
                Birthdate = new DateTime(2005, 3, 1),
                Scholarship = 120.50m,
                GroupId = group.Id,
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
