using FptBookStore.DataAccess.Data;
using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Category;
        }
    }
}
