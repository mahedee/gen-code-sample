using GenericRepo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GenericRepo.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext db = null;
        private DbSet<T> table = null;
        public GenericRepository(ApplicationDbContext context)
        {
            this.db = context;
            table = db.Set<T>();
        }

        public async Task<IEnumerable<T>> SelectAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> SelectByID(object id)
        {
            return await table.FindAsync(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task<int> Save()
        {
           return await db.SaveChangesAsync();
        }
    }
}