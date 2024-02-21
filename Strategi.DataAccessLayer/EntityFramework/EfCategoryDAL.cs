using Strategi.DataAccessLayer.Abstract;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.Repository;
using Strategi.EntityLayer;

namespace Strategi.DataAccessLayer.EntityFramework
{
    public class EfCategoryDAL : GenericRepository<Categories>, ICategoryDAL
    {
        public EfCategoryDAL(AppDbContext context) : base(context)
        {
        }
    }
}
