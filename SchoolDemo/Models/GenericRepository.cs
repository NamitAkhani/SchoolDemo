using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolDemo.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StudentContext _studentContext;
        private DbSet<T> entites;

        public GenericRepository(StudentContext studentContext)
        {
            _studentContext = studentContext;
            entites = _studentContext.Set<T>();
        }

        public void Delete(T obj)
        {
            ShowEntityState(_studentContext);
            entites.Remove(obj);
            ShowEntityState(_studentContext);
        }

        public T GetById(int id )
        {
            //List<T> list = entites..where(expression).Select(u => u).ToList();
            ShowEntityState(_studentContext);
            return entites.Find(id);
            //ShowEntityState(_studentContext);
        }

        public void Insert(T obj)
        {
            ShowEntityState(_studentContext);
            entites.Add(obj);
            ShowEntityState(_studentContext);
        }

        public void Save()
        {
            ShowEntityState(_studentContext);
            _studentContext.SaveChanges();
            ShowEntityState(_studentContext);
        }

        public void Update(T obj)
        {
            ShowEntityState(_studentContext);
            entites.Attach(obj);
            _studentContext.Entry(obj).State = EntityState.Modified;
            ShowEntityState(_studentContext);
        }

        public IQueryable<T> GetAll()
        {
              return entites.AsQueryable();
        }
      
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> resultSet = entites;
            foreach (Expression<Func<T, object>> include in includes)
                resultSet = resultSet.Include(include);

            if (filter != null)
                resultSet = resultSet.Where(filter);

            if (orderBy != null)
                resultSet = orderBy(resultSet);

            return resultSet;
        }
        public static void ShowEntityState(StudentContext studentContext)
        {
            foreach (EntityEntry entry in studentContext.ChangeTracker.Entries())
            {
                _ = ($"type:{entry.Entity.GetType().Name},state:{entry.State}," + $"{entry.Entity}");
            }
        }
       
        //public List<T> GetAllstu(Func<T, bool> filter)
        //{
           // return entites..Where(filter).ToList();
        //}

        //public void Update(T entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public Task Add(T entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void DeleteStudent(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return entites.AsEnumerable();
        //}
    }
}
