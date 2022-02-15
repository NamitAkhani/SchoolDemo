using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolDemo.Models
{
    public interface IGenericRepository<T> where T : class 
    {
        IQueryable<T> GetAll();
        T GetById(int id);
       // Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includes);
        void Insert(T obj);
        void Update(T obj); 
        void Delete(T obj);
        void Save();
      // static void ShowEntityState(StudentContext studentContext);
        //Task Add(T entity); 
        // void DeleteStudent(int id);  
        // void Update(T entity);
    }
}
