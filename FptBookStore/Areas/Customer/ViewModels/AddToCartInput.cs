using System;
namespace FptBookStore.Areas.Customer.ViewModels
{
    public class AddToCartInput
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

        public AddToCartInput()
        {
        }

        public AddToCartInput(int productId, int count)
        {
            ProductId = productId;
            Count = count;
        }
    }
}
