using JwtNet.DataAccess.Abstract;
using JwtNet.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Concrete.EFCore
{
    public abstract class EFCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
         
        public virtual async Task<ResultViewModel<TEntity>> CreateAsync(TEntity entity)
        {
            var result = new ResultViewModel<TEntity>();
            try
            {
                using (var _context = new TContext())
                {
                    _context.Set<TEntity>().Add(entity);
                    var res = await _context.SaveChangesAsync();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "The save is success.";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "The save is'not success.";
                        result.Result = entity;
                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public virtual async Task<ResultViewModel<TEntity>> UpdateAsync(TEntity entity)
        {
            var result = new ResultViewModel<TEntity>();
            try
            {
                using (var _context = new TContext())
                {
                     _context.Set<TEntity>().Update(entity);
                    var res = await _context.SaveChangesAsync();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "The update is success.";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "The update is'not success.";
                        result.Result = entity;
                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public virtual async Task<ResultViewModel<TEntity>> DeleteAsync(TEntity entity)
        {
            var result = new ResultViewModel<TEntity>();
            try
            {
                using (var _context = new TContext())
                {
                    _context.Set<TEntity>().Remove(entity);
                    var res = await _context.SaveChangesAsync();
                    if (res > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "The delete is success.";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "The delete is'not success.";
                        result.Result = entity;
                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public virtual async Task<ResultViewModel<IEnumerable<TEntity>>> GetAllAsync()
        {
            var result = new ResultViewModel<IEnumerable<TEntity>>();
            try
            {
                using (var _context = new TContext())
                {
                    var res= await _context.Set<TEntity>().ToListAsync();
                    if (res.Count>0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Not null.";
                        result.Result = res;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "It is null.";
                        result.Result = res;

                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultViewModel<TEntity>> FindAsync(int id)
        {
            var result = new ResultViewModel<TEntity>();
            
            try
            {
                using (var _context = new TContext())
                {
                    var entity = await _context.Set<TEntity>().FindAsync(id);
                    if (!entity.Equals(null))
                    {
                        result.IsSuccess = true;
                        result.Message = "Find a result.";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Don't find a result.";
                        result.Result = entity;
                    }
                    
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }
        public virtual async Task<ResultViewModel<TEntity>> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] properties)
        {
            var result = new ResultViewModel<TEntity>();
            try
            {
                using (var _context = new TContext())
                {
                    var query = _context.Set<TEntity>().AsQueryable();
                    var navigations = _context.Model.FindEntityType(typeof(TEntity))
                                                            .GetDerivedTypesInclusive()
                                                            .SelectMany(type => type.GetNavigations())
                                                            .Distinct();
                    foreach (var property in navigations)
                    {
                        foreach (string prop in properties)
                        {
                            if (property.Name == prop)
                            {
                                query = query.Include(property.Name);
                            }
                        }
                    }
                    var entity = await query.FirstOrDefaultAsync(predicate);
                    if (!entity.Equals(null))
                    {
                        result.IsSuccess = true;
                        result.Message = "Find a result.";
                        result.Result = entity;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Don't find a result.";
                        result.Result = entity;
                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }

        }

       
    }
}
