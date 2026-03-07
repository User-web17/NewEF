using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF.Entities
{
    public class Passport
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
