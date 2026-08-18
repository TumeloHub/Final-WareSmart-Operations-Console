using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{

    public class Product : IStockTrackable
    {
        public int ProductID { get; private set; }
        public string ProductName { get; private set; }
        public string Category { get; private set; }
        public double Weight { get; private set; }
        public int Quantity { get; private set; }
        public int ReorderLevel { get; private set; }

        public Product(int _productID, string _productName, string _category, double _weight, int _quantity, int _reorderLevel)
        {
            if (string.IsNullOrWhiteSpace(_productName))
                throw new ArgumentException("Product name cannot be empty.");

            if (_weight <= 0)
                throw new ArgumentException("Weight must be greater than zero.");

            if (_quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            if (_reorderLevel < 0)
                throw new ArgumentException("Reorder level cannot be negative.");

            ProductID = _productID;
            ProductName = _productName;
            Category = _category;
            Weight = _weight;
            Quantity = _quantity;
            ReorderLevel = _reorderLevel;
        }
        public void AddStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Quantity += amount;
        }

        public bool RemoveStock(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

            if (amount > Quantity)
            {
                WarehouseLogger.Log("Insufficient stock for product " + ProductName);
                throw new InsufficientStockException("Cannot remove " + amount + " units from " + ProductName + ". Only " + Quantity + " units are available.");
            }

            Quantity -= amount;
            WarehouseLogger.Log(amount + " units removed from " + ProductName);
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



    
    


