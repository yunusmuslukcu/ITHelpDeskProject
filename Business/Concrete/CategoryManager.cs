using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        // IGenericDal yerine ICategoryDal yazıyoruz
        public CategoryManager(ICategoryDal categoryDal) : base(categoryDal)
        {
        }
    }
}
