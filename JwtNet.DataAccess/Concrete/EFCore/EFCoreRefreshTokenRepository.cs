using JwtNet.DataAccess.Abstract;
using JwtNet.DataAccess.Concrete.EFCore.Database;
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Concrete.EFCore
{
    public class EFCoreRefreshTokenRepository : EFCoreRepository<RefreshToken, JwtNetDbContext>, IRefreshTokenRepository
    {
        public ResultViewModel<RefreshToken> GetByToken(string token)
        {
            var result = new ResultViewModel<RefreshToken>();
            try
            {
                using (var _context = new JwtNetDbContext())
                {
                    var refreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == token && x.ExpiryDate > DateTime.Now && x.IsActive);

                    if (refreshToken != null)
                    {
                        result.IsSuccess = true;
                        result.Message = "RefreshToken found";
                        result.Result = refreshToken;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "RefreshToken don't found";
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public ResultViewModel<RefreshToken> GetByUserId(int userId)
        {
            var result = new ResultViewModel<RefreshToken>();
            try
            {
                using (var _context = new JwtNetDbContext())
                {
                    var refreshToken = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId && x.IsActive);

                    if (refreshToken != null)
                    {
                        result.IsSuccess = true;
                        result.Message = "RefreshToken found";
                        result.Result = refreshToken;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "RefreshToken don't found";
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
