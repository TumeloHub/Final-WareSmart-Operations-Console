using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;



namespace ConsoleApp1
{
    
        public class WarehouseOrder
        {
            public int OrderID { get; private set; }
            public string CustomerName { get; private set; }

            private List<Product> products;

            public IReadOnlyList<Product> Products =>
                products.AsReadOnly();

            public WarehouseOrder(int orderID, string customerName)
            {
                if (string.IsNullOrWhiteSpace(customerName))
                    throw new ArgumentException(
                        "Customer name cannot be empty.",
                        nameof(customerName));

                OrderID = orderID;
                CustomerName = customerName;

                products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                products.Add(product);
            }

            public bool RemoveProduct(int productID)
            {
                Product toRemove =
                    products.FirstOrDefault(
                        p => p.ProductID == productID);

                if (toRemove == null)
                    return false;

                products.Remove(toRemove);
                return true;
            }

            public double TotalWeight()
            {
                return products.Sum(
                    p => p.Weight * p.Quantity);
            }

            public override string ToString()
            {
                return $"[Order #{OrderID}] " +
                       $"Customer: {CustomerName} | " +
                       $"Items: {products.Count} | " +
                       $"Total weight: {TotalWeight():F1}kg";
            }
        }
}
