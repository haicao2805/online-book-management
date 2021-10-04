using FptBookStore.DataAccess.BaseRepository;
using FptBookStore.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.DataAccess.IdentityUserRole
{
    public class IdentityUserRoleRepository : Repository<IdentityUserRole<string>>, IIdentityUserRoleRepository
    {
        private readonly ApplicationDbContext _db;
        public IdentityUserRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.UserRoles;
        }
    }
}
