using Strategi.EntityLayer;

namespace Strategi.BusinessLayer.Abstract
{
    public interface IMemberService : IGenericService<Members>
    {
        bool TCheckCredentials(string userName, string password);
        bool TIsAdmin(string userName);
    }
}
