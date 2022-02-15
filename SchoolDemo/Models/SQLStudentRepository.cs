using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDemo.Models
{
    public class SQLStudentRepository:  IStudentRepository
    {
        private readonly StudentContext context;
        public SQLStudentRepository(StudentContext student)
        {
            this.context = student;
        }
        public Student Add(Student student)
        {
            if (student.Id == 0)
            {
                context.Students.Add(student);
            }
            else
            {
                context.Students.Update(student);   
            }
            
            context.SaveChanges();  
            return student;    
        }
        public  IEnumerable<Student> GetAllStudent()
        {   
            return context.Students;
        }
        public Student GetStudent(int Id)
        {
            return context.Students.Find(Id);
        }
        public Student Update(Student StudentChanges)
        {
            var student = context.Students.Attach(StudentChanges);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return StudentChanges;
        }
        public void DeleteStudent(Student student)
        {
            var studentdelete = context.Students.FirstOrDefault(X => X.Id == student.Id);
            context.Students.Remove(studentdelete); 
            context.SaveChanges();  
        }
        public Student EditStudent(Student student)
        {
            var studentedit = context.Students.FirstOrDefault(x=>x.Id == student.Id);
            studentedit.Name = student.Name;    
            studentedit.Fname = student.Fname;  
            studentedit.Email = student.Email;  
            studentedit.Mobile = student.Mobile;
            studentedit.Description = student.Description;  
            studentedit.DepartmentId = student.DepartmentId;    
            studentedit.SemId = student.SemId;
            context.SaveChanges();
            return studentedit;

        }
    }
}
