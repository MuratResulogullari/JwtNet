
using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public class RefreshManager : IRefreshTokenService
    {
        public ResultViewModel Create(RefreshToken entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.RefreshTokens.Add(entity);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "RefreshToken is success save";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "RefreshToken is'not success save";
                    }
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.IsSuccess = false;
                result.Message = "Server Error";
                return result;
            }

        }

        public ResultViewModel Delete(int id)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    var user = _context.RefreshTokens.FirstOrDefault(x => x.Id == id);
                    _context.RefreshTokens.Remove(user);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "RefreshToken is success delete";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "RefreshToken is'not success delete";
                    }
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.IsSuccess = false;
                result.Message = "Server Error";
                return result;
            }
        }

        public RefreshToken GetById(int id)
        {
            try
            {
                using (var _context = new ApplicationContext())
                {
                    var user = _context.RefreshTokens.FirstOrDefault(x => x.Id == id);
                    return user;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultViewModel Update(RefreshToken entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.RefreshTokens.Update(entity);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "RefreshToken is success update";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "RefreshToken is'not success update";
                    }
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.IsSuccess = false;
                result.Message = "Server Error";
                return result;
            }
        }
    }
}
