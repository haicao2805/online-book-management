using FptBookStore.DataAccess.Data;
using FptBookStore.DataAccess.Categories;
using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FptBookStore.DataAccess.BaseRepository;

namespace FptBookStore.DataAccess.OrderDetails
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.OrderDetail;
        }
    }
}
