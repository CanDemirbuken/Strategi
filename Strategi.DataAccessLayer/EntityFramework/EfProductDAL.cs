using Strategi.DataAccessLayer.Abstract;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.Repository;
using Strategi.EntityLayer;

namespace Strategi.DataAccessLayer.EntityFramework
{
    public class EfProductDAL : GenericRepository<Products>, IProductDAL
    {
        public EfProductDAL(AppDbContext context) : base(context)
        {
        }
    }
}
