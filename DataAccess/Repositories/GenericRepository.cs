using DataAccess.Abstract; // Kendi namespace'ine göre kontrol et
using DataAccess.Context;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {
        public void Delete(T entity)
        {
            using var c = new AppDbContext();
            c.Remove(entity);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            using var c = new AppDbContext();
            return c.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var c = new AppDbContext();
            return c.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            using var c = new AppDbContext();
            c.Add(entity);
            c.SaveChanges();
        }

        public void Update(T entity)
        {
            using var c = new AppDbContext();
            c.Update(entity);
            c.SaveChanges();
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            using var c = new AppDbContext();
            return c.Set<T>().Where(filter).ToList();
        }
    }
}