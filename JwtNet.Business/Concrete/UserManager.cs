
using JwtNet.DataAccess.Abstract;
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using System.Linq.Expressions;

namespace JwtNet.Business.Abstract
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public Task<ResultViewModel<User>> CreateAsync(User entity)
        {
           return _userRepository.CreateAsync(entity);
        }

        public Task<ResultViewModel<User>> DeleteAsync(User entity)
        {
            return _userRepository.DeleteAsync(entity);
        }

        public Task<ResultViewModel<User>> FindAsync(int id)
        {
            return _userRepository.FindAsync(id);  
        }

        public Task<ResultViewModel<User>> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate, string[] properties)
        {
            return _userRepository.FirstOrDefaultAsync(predicate, properties);
        }

        public Task<ResultViewModel<IEnumerable<User>>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public ResultViewModel<User> GetByUserName(string userName)
        {
            return _userRepository.GetByUserName(userName); 
        }

        public Task<ResultViewModel<User>> UpdateAsync(User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }
    }
}
