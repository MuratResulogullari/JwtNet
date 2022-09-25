using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public interface IUserService:IService<User>
    {
        ResultViewModel GetByUserName(string userName);
    }
}
