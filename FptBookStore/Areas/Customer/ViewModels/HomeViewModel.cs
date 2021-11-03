using System;
using System.Collections.Generic;
using FptBookStore.Entities;

namespace FptBookStore.Areas.Customer.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> NewestProducts { get; set; }
        public String Message { get; set; }


        public HomeViewModel()
        {

        }

        public HomeViewModel(List<Product> products, List<Category> categories, string message, List<Product> newestProducts)
        {
            Products = products;
            Categories = categories;
            Message = message;
            NewestProducts = newestProducts;
        }

        public HomeViewModel(List<Product> products, List<Category> categories, List<Product> newestProducts)
        {
            Products = products;
            Categories = categories;
            NewestProducts = newestProducts;
        }

    }
}
