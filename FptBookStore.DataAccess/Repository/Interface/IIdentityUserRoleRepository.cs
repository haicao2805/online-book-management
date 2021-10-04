using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.DataAccess.Repository.Interface
{
    public interface IIdentityUserRoleRepository : IRepository<IdentityUserRole<string>>
    {
    }
}
