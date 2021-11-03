using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.DataAccess.Data;
using FptBookStore.DataAccess.IdentityUserRole;
using FptBookStore.DataAccess.OrderDetails;
using FptBookStore.DataAccess.Orders;
using FptBookStore.DataAccess.Products;
using FptBookStore.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.DataAccess.Categories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IIdentityRoleRepository IdentityRole { get; private set; }
        public IIdentityUserRoleRepository IdentityUserRole { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        IApplicationUserRepository IUnitOfWork.ApplicationUser => ApplicationUser;

        IIdentityRoleRepository IUnitOfWork.IdentityRole => IdentityRole;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            IdentityRole = new IdentityRoleRepository(_db);
            IdentityUserRole = new IdentityUserRoleRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            SP_Call = new SP_Call(_db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IIdentityUserRoleRepository GetIdentityUserRole()
        {
            throw new NotImplementedException();
        }
    }
}
