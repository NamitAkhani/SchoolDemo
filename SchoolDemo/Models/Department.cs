using System.Collections.Generic;

namespace SchoolDemo.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
