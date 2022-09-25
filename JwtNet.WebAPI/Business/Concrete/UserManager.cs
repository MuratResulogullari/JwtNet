using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public class UserManager : IUserService
    {
       
        public ResultViewModel Create(User entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.Users.Add(entity);
                    var res = _context.SaveChanges();
                    if (res>0)
                    {
                        result.IsSuccess = true;
                        result.Message = "User is success save";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "User is'not success save";
                    }
                    return result;
                }
                

            }
            catch (Exception ex)
            {
                result.Result=ex.Message;
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
                    var user = _context.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
                    _context.Users.Remove(user);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "User is success delete";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "User is'not success delete";
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

        public User GetById(int id)
        {
            try
            {
                using (var _context = new ApplicationContext())
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
                    return user;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultViewModel GetByUserName(string userName)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    var user = _context.Users.FirstOrDefault(x=>x.UserName==userName && x.IsActive);
                    if (user!=null)
                    {
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
                result.Result = ex.Message;
                result.IsSuccess = false;
                result.Message = "Server Error.";
                return result;
            }
        }

        public ResultViewModel Update(User entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.Users.Update(entity);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "User is success update";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "User is'not success update";
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
