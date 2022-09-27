using JwtNet.DataAccess.Abstract;
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using System.Linq.Expressions;

namespace JwtNet.Business.Abstract
{
    public class RoleManager : IRoleService
    {
        private IRoleRepository _roleRepository;
        public RoleManager(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public Task<ResultViewModel<Role>> CreateAsync(Role entity)
        {
            return _roleRepository.CreateAsync(entity);
        }

        public Task<ResultViewModel<Role>> DeleteAsync(Role entity)
        {
           return _roleRepository.DeleteAsync(entity);
        }

        public Task<ResultViewModel<Role>> FindAsync(int id)
        {
            return _roleRepository.FindAsync(id);
        }

        public Task<ResultViewModel<Role>> FirstOrDefaultAsync(Expression<Func<Role, bool>> predicate, string[] properties)
        {
            return _roleRepository.FirstOrDefaultAsync(predicate, properties);
        }

        public Task<ResultViewModel<IEnumerable<Role>>> GetAllAsync()
        {
            return _roleRepository.GetAllAsync();
        }

        public Task<ResultViewModel<Role>> UpdateAsync(Role entity)
        {
            return _roleRepository.UpdateAsync(entity);
        }
    }
}
