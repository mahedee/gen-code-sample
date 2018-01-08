using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepo.Repository
{
    interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectByID(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        Task<int> Save();
    }
}
