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

            CreateGroup(db);
            CreateStudent(db);
            CreateTeacher(db);
            CreateSubject(db);

            ReadStudents(db);

            UpdateStudent(db, 1);

            DeleteStudent(db, 2);

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

        static void CreateGroup(AppDbContext db)
        {
            string groupName = "LOL-606";

            var existingGroup = db.Groups.FirstOrDefault(g => g.Name == groupName);

            if (existingGroup != null)
            {
                Console.WriteLine("Group already exists.");
                return;
            }

            var group = new Group
            {
                Name = groupName,
                Students = new List<Student>()
            };

            db.Groups.Add(group);
            db.SaveChanges();

            Console.WriteLine("Group created.");
        }

        static void ReadGroups(AppDbContext db)
        {
            var groups = db.Groups
                           .Include(g => g.Students)
                           .ToList();

            foreach (var g in groups)
            {
                Console.WriteLine(g.ToString());
            }
        }

        static void UpdateGroup(AppDbContext db, int id)
        {
            var group = db.Groups.FirstOrDefault(g => g.Id == id);

            if (group == null) return;

            group.Name = "UPDATED-GROUP";

            db.Groups.Update(group);
            db.SaveChanges();

            Console.WriteLine("Group updated.");
        }

        static void DeleteGroup(AppDbContext db, int id)
        {
            var group = db.Groups.FirstOrDefault(g => g.Id == id);

            if (group == null) return;

            db.Groups.Remove(group);
            db.SaveChanges();

            Console.WriteLine("Group deleted.");
        }

        static void CreateStudent(AppDbContext db)
        {
            string email = "lol131.yo@gmail.com";

            var existingStudent = db.Students.FirstOrDefault(s => s.Email == email);

            if (existingStudent != null)
            {
                Console.WriteLine("Student already exists.");
                return;
            }

            var group = db.Groups.FirstOrDefault();

            if (group == null)
            {
                Console.WriteLine("No groups found.");
                return;
            }

            var student = new Student
            {
                FirstName = "Mihaylo",
                LastName = "Student",
                Email = email,
                GroupId = group.Id
            };

            db.Students.Add(student);
            db.SaveChanges();

            Console.WriteLine("Student created.");
        }

        static void ReadStudents(AppDbContext db)
        {
            var students = db.Students
                .Include(s => s.Group)
                .Include(s => s.Passport)
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine(
                    $"{s.Id} | {s.FirstName} {s.LastName} | {s.Email} | " +
                    $"{s.Group?.Name} | {s._attendanceForm}"
                );

                if (s.Passport != null)
                {
                    Console.WriteLine($"Passport: {s.Passport.Number}");
                }
            }
        }

        static void UpdateStudent(AppDbContext db, int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null) return;

            student.Scholarship = 500;

            db.Students.Update(student);
            db.SaveChanges();

            Console.WriteLine("Student updated.");
        }

        static void DeleteStudent(AppDbContext db, int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null) return;

            db.Students.Remove(student);
            db.SaveChanges();

            Console.WriteLine("Student deleted.");
        }


        static void CreateTeacher(AppDbContext db)
        {
            var dep = db.Departments.First();

            var teacher = new Teacher
            {
                FirstName = "Some",
                LastName = "New",
                BirthDate = new DateTime(1980, 1, 1),
                Salary = 4000,
                DepartmentId = dep.Id,
                Subjects = new List<Subject>(),
                Groups = new List<Group>()
            };

            db.Teachers.Add(teacher);
            db.SaveChanges();

            Console.WriteLine("Teacher created.");
        }

        static void UpdateTeacher(AppDbContext db, int id)
        {
            var teacher = db.Teachers.FirstOrDefault(t => t.Id == id);

            if (teacher == null) return;

            teacher.Salary = 5000;

            db.Teachers.Update(teacher);
            db.SaveChanges();

            Console.WriteLine("Teacher updated.");
        }

        static void DeleteTeacher(AppDbContext db, int id)
        {
            var teacher = db.Teachers.FirstOrDefault(t => t.Id == id);

            if (teacher == null) return;

            db.Teachers.Remove(teacher);
            db.SaveChanges();

            Console.WriteLine("Teacher deleted.");
        }

        static void CreateSubject(AppDbContext db)
        {
            var dep = db.Departments.First();

            var subject = new Subject
            {
                Name = "Horeography",
                Description = "Basic Moves",
                DepartmentId = dep.Id,
                Teachers = new List<Teacher>()
            };

            db.Subjects.Add(subject);
            db.SaveChanges();

            Console.WriteLine("Subject created.");
        }

        static void UpdateSubject(AppDbContext db, int id)
        {
            var subject = db.Subjects.FirstOrDefault(s => s.Id == id);

            if (subject == null) return;

            subject.Description = "Updated description";

            db.Subjects.Update(subject);
            db.SaveChanges();

            Console.WriteLine("Subject updated.");
        }

        static void DeleteSubject(AppDbContext db, int id)
        {
            var subject = db.Subjects.FirstOrDefault(s => s.Id == id);

            if (subject == null) return;

            db.Subjects.Remove(subject);
            db.SaveChanges();

            Console.WriteLine("Subject deleted.");
        }
    }
}
