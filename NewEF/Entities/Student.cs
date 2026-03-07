using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NewEF.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id}. {FirstName} {LastName} {Email} {Birthday:d} -- {Group.Name}";
        }
    }
}
