
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;

namespace JwtNet.Business.Abstract
{
    public interface IRefreshTokenService : IService<RefreshToken>
    {
        ResultViewModel<RefreshToken> GetByUserId(int userId);
        ResultViewModel<RefreshToken> GetByToken(string token);
    }
}
