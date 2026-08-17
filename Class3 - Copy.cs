using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{

    public class Product
    {
        public int ProductID { get; private set; }
        public string ProductName { get; private set; }
        public string Category { get; private set; }
        public double Weight { get; private set; }
        public int Quantity { get; private set; }
        public int ReorderLevel { get; private set; }

        public Product(
            int productID,
            string productName,
            string category,
            double weight,
            int quantity,
            int reorderLevel)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name cannot be empty.");

            if (weight <= 0)
                throw new ArgumentException("Weight must be greater than zero.");

            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            if (reorderLevel < 0)
                throw new ArgumentException("Reorder level cannot be negative.");

            ProductID = productID;
            ProductName = productName;
            Category = category;
            Weight = weight;
            Quantity = quantity;
            ReorderLevel = reorderLevel;
        }
        public void AddStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Quantity += amount;
        }

        public bool RemoveStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            if (amount > Quantity)
                return false;

            Quantity -= amount;
            return true;
        }

        public bool NeedsRestocking()
        {
            return Quantity <= ReorderLevel;
        }

        public override string ToString()
        {
            return $"Product #{ProductID} | " +
                   $"{ProductName} | " +
                   $"Category: {Category} | " +
                   $"Stock: {Quantity} | " +
                   $"Weight: {Weight:F1}kg";
        }




    }
}



    
    


