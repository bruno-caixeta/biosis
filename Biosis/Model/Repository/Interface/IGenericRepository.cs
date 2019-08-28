using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
        T GetOne(Guid id);
        List<T> GetAll();
    }
}
