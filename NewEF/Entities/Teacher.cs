using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF.Entities
{
     public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public List<Group> Groups { get; set; } = null!;
        public List<Subject> Subjects { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id} | {FirstName} | {LastName} | {Salary} |" +
                $" {BirthDate:d} | Subjects count: {Subjects.Count} | Groups count: {Groups.Count}";
        }
    }
}
