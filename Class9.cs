using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    // Represents a physical warehouse which has its own inventory,
    // a task manager and a collection of orders.
    public class Warehouse
    {
        public string WarehouseName { get; private set; }

        public Inventory Inventory { get; private set; }

        public List<WarehouseOrder> Orders { get; private set; }

        public WarehouseTaskManager TaskManager { get; private set; }

        public Warehouse(string warehouseName)
        {
            if (string.IsNullOrWhiteSpace(warehouseName))
                throw new ArgumentException("Warehouse name cannot be empty.");

            WarehouseName = warehouseName;

            Inventory = new Inventory();

            Orders = new List<WarehouseOrder>();

            TaskManager = new WarehouseTaskManager();
        }

        public void AddOrder(WarehouseOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            Orders.Add(order);
        }

        public bool RemoveOrder(int orderID)
        {
            WarehouseOrder order = Orders.Find(o => o.OrderID == orderID);

            if (order == null)
                return false;

            Orders.Remove(order);
            return true;
        }

        public WarehouseOrder GetOrderByID(int orderID)
        {
            return Orders.Find(o => o.OrderID == orderID);
        }

        public override string ToString()
        {
            return $"{WarehouseName} | " +
                   $"Products: {Inventory.ProductCount} | " +
                   $"Orders: {Orders.Count} | " +
                   $"Tasks: {TaskManager.Count}";
        }
    }
}





