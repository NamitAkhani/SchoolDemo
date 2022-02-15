using System.Collections.Generic;

namespace SchoolDemo.Models
{
    public class Sem
    {
        public int SemId { get; set; }
        public string SemName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
