using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Inventory
    {
        private List<Product> products;

        public Inventory()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            WarehouseValidator.ValidateProduct(product);

            if (products.Any(p => p.ProductID == product.ProductID)) throw new ArgumentException("A product with this ID already exists.");

            products.Add(product);
            WarehouseLogger.Log("Product added: " + product.ProductName);
        }

        public bool RemoveProduct(int productID)
        {
            Product product = products.FirstOrDefault(
                p => p.ProductID == productID);

            if (product == null)
                return false;

            products.Remove(product);
            return true;
        }

        public Product GetProductByID(int productID)
        {
            return products.FirstOrDefault(
                p => p.ProductID == productID);
        }

        public IReadOnlyList<Product> GetAllProducts()
        {
            return products.AsReadOnly();
        }

        public int ProductCount => products.Count;
    }

}
