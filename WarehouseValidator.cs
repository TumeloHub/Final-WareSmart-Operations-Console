using System;

namespace ConsoleApp1
{
    // Contains validation rules used by the warehouse system.
    public static class WarehouseValidator
    {
        public static void ValidateProduct(Product product)
        {
            if (product == null) {
                throw new ArgumentNullException(nameof(product));
            }
            if (product.Quantity < 0) {
                throw new ArgumentException("Product quantity cannot be negative.");
            }
            if (product.Weight <= 0) {
                throw new ArgumentException("Product weight must be greater than zero.");
            }
        }

        public static void ValidateOrder(WarehouseOrder order)
        {
            if (order == null) {
                throw new ArgumentNullException(nameof(order));
            }
            if (order.Products.Count > 50) {
                throw new ArgumentException("An order cannot contain more than 50 products.");
            }
        }
    }
}
