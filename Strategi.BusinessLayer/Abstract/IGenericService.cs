using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategi.BusinessLayer.Abstract
{
    public interface IGenericService<TEntity>
    {
        void TAdd(TEntity t);
        void TDelete(TEntity t);
        void TUpdate(TEntity t);
        List<TEntity> TGetList();
        TEntity TGetByID(int id);
    }
}
