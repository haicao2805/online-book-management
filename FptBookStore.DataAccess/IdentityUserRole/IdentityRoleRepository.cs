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
    public class IdentityRoleRepository : Repository<IdentityRole>, IIdentityRoleRepository
    {
        private readonly ApplicationDbContext _db;
        public IdentityRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Roles;
        }
    }
}
