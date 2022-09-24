using JwtNet.WebAPI.Models.Entities;
using JwtNet.WebAPI.Models.ViewModels;

namespace JwtNet.WebAPI.Business.Abstract
{
    public class RoleManager : IRoleService
    {
        public ResultViewModel Create(Role entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.Roles.Add(entity);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Role is success save";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Role is'not success save";
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
                    var user = _context.Roles.FirstOrDefault(x => x.Id == id);
                    _context.Roles.Remove(user);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Role is success delete";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Role is'not success delete";
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

        public Role GetById(int id)
        {
            try
            {
                using (var _context = new ApplicationContext())
                {
                    var user = _context.Roles.FirstOrDefault(x => x.Id == id);
                    return user;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultViewModel Update(Role entity)
        {
            ResultViewModel result = new ResultViewModel();
            try
            {
                using (var _context = new ApplicationContext())
                {
                    _context.Roles.Update(entity);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Role is success update";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Role is'not success update";
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
