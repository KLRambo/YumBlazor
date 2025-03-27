using Microsoft.CodeAnalysis.CSharp.Syntax;
using YumBlazor.Data;

namespace YumBlazor.Utility
{
    public static class SD
    {
        public static string Role_Admin = "Admin";
        public static string Role_Customer = "Customer";
        public static string StatusPending = "Pending";
        public static string StatusReadyForPickUp = "ReadyForPickUp";
        public static string StatusCompleted = "Completed";
        public static string StatusCancelled = "Cancelled";

        public static List<OrderDetail> ConvertShoppingCArtListToOrderDetail (List<ShoppingCart> shoppingCarts)
        {
            List<OrderDetail> orderDetails = new();
            
            foreach(var cart in shoppingCarts)
            {
                OrderDetail order = new OrderDetail
                {
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    Price = (double)cart.Product.Price,
                    ProductName = cart.Product.Name
                };

                orderDetails.Add(order);
            }

            return orderDetails;

        }
    }
}
