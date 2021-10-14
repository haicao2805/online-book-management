using FptBookStore.DataAccess.Data;

using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Product;
        }
    }
}
