using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class, new()
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetList();
        T GetById(int id);

        // Bu biraz "pro" bir satır, ama çok işimize yarayacak:
        // Şartlı listeleme (Örn: Sadece silinmemişleri getir gibi)
        List<T> GetListByFilter(Expression<Func<T, bool>> filter);
    }
}
