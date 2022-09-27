using JwtNet.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        Task<ResultViewModel<T>> CreateAsync(T entity);
        Task<ResultViewModel<T>> UpdateAsync(T entity);
        Task<ResultViewModel<T>> DeleteAsync(T entity);
        Task<ResultViewModel<IEnumerable<T>>> GetAllAsync();
        Task<ResultViewModel<T>> FindAsync(int id);
        Task<ResultViewModel<T>> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] properties);
        //Task<ResultViewModel> SelectAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> selector);
        //Task<ResultViewModel<IEnumerable<T>>> Where(Expression<Func<T, bool>> predicate);
        //Task<ResultViewModel<bool>> Any(Expression<Func<T, bool>> predicate);
        //Task<ResultViewModel<int>> CountAsync(Expression<Func<T, bool>> predicate);
        //Task<ResultViewModel<int>> MaxAsync(Expression<Func<T, bool>> predicate);
        //Task<ResultViewModel<int>> MinAsync(Expression<Func<T, bool>> predicate);
        //Task<ResultViewModel<int>> AvgAsync(Expression<Func<T, bool>> predicate);

    }
}
