using FptBookStore.DataAccess.Data;

using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.ShoppingCarts
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.ShoppingCart;
        }
    }
}
