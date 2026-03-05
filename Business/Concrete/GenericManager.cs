using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class, new()
    {
        private readonly IGenericDal<T> _repository;  //Dependency Injection

        public GenericManager(IGenericDal<T> repository)
        {
            _repository = repository;
        }
        public void TDelete(T t) => _repository.Delete(t);

        public T TGetById(int id) => _repository.GetById(id);

        public List<T> TGetList() => _repository.GetList();

        public List<T> TGetListByFilter(Expression<Func<T, bool>> filter) => _repository.GetListByFilter(filter);

        public void TInsert(T t) => _repository.Insert(t);

        public void TUpdate(T t) => _repository.Update(t);
    }
}
