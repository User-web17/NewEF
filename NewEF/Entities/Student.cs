using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NewEF.Entities
{
    public class Student
    {
        public enum AttendanceForm
        {
            Online, Offline, Hybrid
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public decimal Scholarship { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public int PassportId { get; set; }
        public Passport Passport { get; set; } = null!;

        public AttendanceForm _attendanceForm { get; set; }
        public override string ToString()
        {
            return $"{Id}. {FirstName} {LastName} {Email} {Scholarship} {Birthdate:d} {_attendanceForm} {PassportId} -- {Group.Name}";
        }
    }
}
