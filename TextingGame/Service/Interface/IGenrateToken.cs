using Persistence.Model;

namespace Service.Interface
{
    public interface IGenrateToken
    {
        string GenerateToken(TblUser user);
    }
}
