using FptBookStore.DataAccess.BaseRepository.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.DataAccess.IdentityUserRole
{
    public interface IIdentityUserRoleRepository : IRepository<IdentityUserRole<string>>
    {
    }
}
