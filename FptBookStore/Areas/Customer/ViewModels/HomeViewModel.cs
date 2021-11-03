using System;
using System.Collections.Generic;
using FptBookStore.Entities;

namespace FptBookStore.Areas.Customer.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public String Message { get; set; }


        public HomeViewModel()
        {

        }

        public HomeViewModel(List<Product> products, List<Category> categories, string message)
        {
            Products = products;
            Categories = categories;
            Message = message;
        }

        public HomeViewModel(List<Product> products, List<Category> categories)
        {
            Products = products;
            Categories = categories;
        }

    }
}
