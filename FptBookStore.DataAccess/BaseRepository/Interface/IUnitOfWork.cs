using FptBookStore.DataAccess.Categories;
using FptBookStore.DataAccess.Company;
using FptBookStore.DataAccess.IdentityUserRole;
using FptBookStore.DataAccess.OrderDetails;
using FptBookStore.DataAccess.Orders;
using FptBookStore.DataAccess.Products;
using FptBookStore.DataAccess.ShoppingCarts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FptBookStore.DataAccess.BaseRepository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IIdentityUserRoleRepository IdentityUserRole { get; }
        IIdentityRoleRepository IdentityRole { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        ISP_Call SP_Call { get; }

        public void Save();
    }
}
