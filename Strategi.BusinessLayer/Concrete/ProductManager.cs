using Strategi.BusinessLayer.Abstract;
using Strategi.DataAccessLayer.Abstract;
using Strategi.EntityLayer;
using System.Collections.Generic;

namespace Strategi.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public void TAdd(Products t)
        {
            _productDAL.Add(t);
        }

        public void TDelete(Products t)
        {
            _productDAL.Delete(t);
        }

        public Products TGetByID(int id)
        {
            return _productDAL.GetByID(id);
        }

        public List<Products> TGetList()
        {
            return _productDAL.GetAll();
        }

        public void TUpdate(Products t)
        {
            _productDAL.Update(t);
        }
    }
}
