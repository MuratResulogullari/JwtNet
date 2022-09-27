using JwtNet.DataAccess.Abstract;
using JwtNet.DataAccess.Concrete.EFCore.Database;
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Concrete.EFCore
{
    public class EFCoreUserRepository : EFCoreRepository<User, JwtNetDbContext>, IUserRepository
    {
        public ResultViewModel<User> GetByUserName(string userName)
        {
            var result = new ResultViewModel<User>();
            try
            {
                using (var _context = new JwtNetDbContext())
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(userName) && x.IsActive);
                    if (user != null)
                    {
                        user.Role = _context.Roles.FirstOrDefault(r => r.Id == user.Id && r.IsActive);
                        result.IsSuccess = true;
                        result.Message = "User found.";
                        result.Result = user;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "User not found.";
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
