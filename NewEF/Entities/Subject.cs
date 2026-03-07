using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<Teacher> Teachers { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id} | {Name} | {Description} | Teacher count: {Teachers.Count}";
        }
    }
}
