using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public interface IService<T>
    {
        ResultViewModel Create(T entity);
        ResultViewModel Update(T entity);
        ResultViewModel Delete(int id);
        T GetById(int id);
    }
}
