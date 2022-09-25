
using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public interface IRefreshTokenService : IService<RefreshToken>
    {
        ResultViewModel GetByUserId(int userId);
        ResultViewModel GetByToken(string token);
    }
}
