using Strategi.DataAccessLayer.Abstract;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.Repository;
using Strategi.EntityLayer;
using System.Linq;

namespace Strategi.DataAccessLayer.EntityFramework
{
    public class EfMemberDAL : GenericRepository<Members>, IMemberDAL
    {
        public EfMemberDAL(AppDbContext context) : base(context)
        {
        }

        public bool CheckCredentials(string userName, string password)
        {
            var user = _context.Members.FirstOrDefault(m => m.UserName == userName && m.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAdmin(string userName)
        {
            var user = _context.Members.FirstOrDefault(m => m.UserName == userName);
            if (user.Is_Admin == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
