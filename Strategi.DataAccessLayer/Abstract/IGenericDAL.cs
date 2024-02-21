using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategi.DataAccessLayer.Abstract
{
    public interface IGenericDAL<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetByID(int id);
        void Add(TEntity t);
        void Update(TEntity t);
        void Delete(TEntity t);
    }
}
