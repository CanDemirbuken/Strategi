using Strategi.BusinessLayer.Abstract;
using Strategi.DataAccessLayer.Abstract;
using Strategi.EntityLayer;
using System.Collections.Generic;

namespace Strategi.BusinessLayer.Concrete
{
    public class MemberManager : IMemberService
    {
        IMemberDAL _memberDAL;

        public MemberManager(IMemberDAL memberDAL)
        {
            _memberDAL = memberDAL;
        }

        public void TAdd(Members t)
        {
            _memberDAL.Add(t);
        }

        public void TDelete(Members t)
        {
            _memberDAL.Delete(t);
        }

        public Members TGetByID(int id)
        {
            return _memberDAL.GetByID(id);
        }

        public List<Members> TGetList()
        {
            return _memberDAL.GetAll();
        }

        public void TUpdate(Members t)
        {
            _memberDAL.Update(t);
        }

        public bool TCheckCredentials(string userName, string password)
        {
            return _memberDAL.CheckCredentials(userName, password);
        }

        public bool TIsAdmin(string userName)
        {
            return _memberDAL.IsAdmin(userName);
        }
    }
}
