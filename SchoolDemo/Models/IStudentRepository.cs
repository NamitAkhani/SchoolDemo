using System.Collections.Generic;

namespace SchoolDemo.Models
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudent();
        Student Add(Student employee);
        Student GetStudent(int Id);
        Student Update(Student StudentChanges);
        void DeleteStudent(Student employee);
        Student EditStudent(Student student);
    }
   
}
