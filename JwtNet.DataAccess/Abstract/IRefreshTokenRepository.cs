using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Abstract
{
    public interface IRefreshTokenRepository:IRepository<RefreshToken>
    {
        ResultViewModel<RefreshToken> GetByUserId(int userId);
        ResultViewModel<RefreshToken> GetByToken(string token);
    }
}
