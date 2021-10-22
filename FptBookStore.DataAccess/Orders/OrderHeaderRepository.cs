using FptBookStore.DataAccess.Data;

using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.Orders
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.OrderHeader;
        }
    }
}
