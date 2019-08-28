using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public void Delete(T entity)
        {
            using (var db = new DataContext())
            {
                db.Set<T>();
                db.Remove(entity);
                db.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            using (var db = new DataContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public T GetOne(Guid id)
        {
            using (var db = new DataContext())
            {
                return db.Set<T>().Find(id);
            }
        }

        public T Insert(T entity)
        {
            using (var db = new DataContext())
            {
                db.Set<T>();
                db.Add(entity);
                db.SaveChanges();
                return entity;
            }
        }

        public T Update(T entity)
        {
            using (var db = new DataContext())
            {
                db.Set<T>();
                db.Update(entity);
                db.SaveChanges();
                return entity;
            }
        }
    }
}
