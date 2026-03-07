using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Teacher> Teachers { get; set; } = null!;
        public List<Subject> Subjects { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id} | {Name} | {Description} | Teachers count: {Teachers.Count} | Subjects count: {Subjects.Count}";
        }
    }
}
