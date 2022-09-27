using JwtNet.Entities.ViewModels;
using System.Linq.Expressions;

namespace JwtNet.Business.Abstract
{
    public interface IService<T>
    {
        Task<ResultViewModel<T>> CreateAsync(T entity);
        Task<ResultViewModel<T>> UpdateAsync(T entity);
        Task<ResultViewModel<T>> DeleteAsync(T entity);
        Task<ResultViewModel<IEnumerable<T>>> GetAllAsync();
        Task<ResultViewModel<T>> FindAsync(int id);
        Task<ResultViewModel<T>> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] properties);

    }
}
