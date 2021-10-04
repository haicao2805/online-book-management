using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
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
