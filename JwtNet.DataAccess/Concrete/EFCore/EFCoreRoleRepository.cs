using JwtNet.DataAccess.Abstract;
using JwtNet.DataAccess.Concrete.EFCore.Database;
using JwtNet.Entities.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtNet.DataAccess.Concrete.EFCore
{
    public class EFCoreRoleRepository :EFCoreRepository<Role,JwtNetDbContext> ,IRoleRepository
    {
    }
}
