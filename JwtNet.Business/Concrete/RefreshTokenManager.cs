
using JwtNet.DataAccess.Abstract;
using JwtNet.Entities.DbModels;
using JwtNet.Entities.ViewModels;
using System.Linq.Expressions;

namespace JwtNet.Business.Abstract
{
    public class RefreshTokenManager : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenManager(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public Task<ResultViewModel<RefreshToken>> CreateAsync(RefreshToken entity)
        {
                return _refreshTokenRepository.CreateAsync(entity);
        }

        public Task<ResultViewModel<RefreshToken>> DeleteAsync(RefreshToken entity)
        {
            return _refreshTokenRepository.DeleteAsync(entity);
        }

        public Task<ResultViewModel<RefreshToken>> FindAsync(int id)
        {
           return _refreshTokenRepository.FindAsync(id);
        }

        public Task<ResultViewModel<RefreshToken>> FirstOrDefaultAsync(Expression<Func<RefreshToken, bool>> predicate, string[] properties)
        {
           return _refreshTokenRepository.FirstOrDefaultAsync(predicate, properties);  
        }

        public Task<ResultViewModel<IEnumerable<RefreshToken>>> GetAllAsync()
        {
            return _refreshTokenRepository.GetAllAsync();
        }

        public ResultViewModel<RefreshToken> GetByToken(string token)
        {
            return _refreshTokenRepository.GetByToken(token);
        }

        public ResultViewModel<RefreshToken> GetByUserId(int userId)
        {
            return _refreshTokenRepository.GetByUserId(userId);
        }

        public Task<ResultViewModel<RefreshToken>> UpdateAsync(RefreshToken entity)
        {
            return _refreshTokenRepository.UpdateAsync(entity);
        }
    }
}
