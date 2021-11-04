using System;
using System.Collections.Generic;
using FptBookStore.Entities;
using PagedList;

namespace FptBookStore.Areas.Customer.ViewModels
{
    public class HomeViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> NewestProducts { get; set; }
        public String Message { get; set; }
        public AddToCartInput AddToCartInput { get; set; }


        public HomeViewModel()
        {
        }

        public HomeViewModel(IPagedList<Product> products, List<Category> categories, string message, List<Product> newestProducts)
        {
            Products = products;
            Categories = categories;
            Message = message;
            NewestProducts = newestProducts;
        }

        public HomeViewModel(IPagedList<Product> products, List<Category> categories, List<Product> newestProducts)
        {
            Products = products;
            Categories = categories;
            NewestProducts = newestProducts;
        }

    }
}
