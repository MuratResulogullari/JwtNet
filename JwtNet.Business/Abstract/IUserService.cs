
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;

namespace JwtNet.Business.Abstract
{
    public interface IUserService:IService<User>
    {
        ResultViewModel<User> GetByUserName(string userName);
    }
}
