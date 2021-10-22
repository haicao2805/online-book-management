using FptBookStore.DataAccess.BaseRepository.Interface;

using FptBookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FptBookStore.DataAccess.ShoppingCarts
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
    }
}
