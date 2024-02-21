using Strategi.EntityLayer;

namespace Strategi.DataAccessLayer.Abstract
{
    public interface IMemberDAL : IGenericDAL<Members>
    {
        bool CheckCredentials(string userName, string Password);
        bool IsAdmin(string userName);
    }
}
