using System;
using FptBookStore.Entities;

namespace FptBookStore.Areas.Customer.ViewModels
{
    public class DetailViewModel
    {
        public Product Product { get; set; }
        public String Message { get; set; }
        public AddToCartInput AddToCartInput { get; set; }

        public DetailViewModel(AddToCartInput addToCartInput)
        {
            AddToCartInput = addToCartInput;
        }

        public DetailViewModel()
        {
        }

        public DetailViewModel(Product product)
        {
            Product = product;
        }

        public DetailViewModel(Product product, string message)
        {
            Product = product;
            Message = message;
        }

        public DetailViewModel(string message, AddToCartInput addToCartInput)
        {
            Message = message;
            AddToCartInput = addToCartInput;
        }
    }
}
