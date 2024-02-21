using Strategi.BusinessLayer.Abstract;
using Strategi.DataAccessLayer.Abstract;
using Strategi.EntityLayer;
using System.Collections.Generic;

namespace Strategi.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public void TAdd(Categories t)
        {
            _categoryDAL.Add(t);
        }

        public void TDelete(Categories t)
        {
            _categoryDAL.Delete(t);
        }

        public Categories TGetByID(int id)
        {
            return _categoryDAL.GetByID(id);
        }

        public List<Categories> TGetList()
        {
            return _categoryDAL.GetAll();
        }

        public void TUpdate(Categories t)
        {
            _categoryDAL.Update(t);
        }
    }
}
