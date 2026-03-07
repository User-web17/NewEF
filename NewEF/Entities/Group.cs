using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<Student> Students { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id}. {Name} -- {Students.Count}";
        }
    }
}
