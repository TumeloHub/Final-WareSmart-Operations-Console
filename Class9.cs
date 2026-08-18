using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    // Represents a physical warehouse which has its own inventory,
    // a task manager, orders and the background monitoring service.
    public class Warehouse
    {
        public string WarehouseName { get; private set; }

        public Inventory Inventory { get; private set; }

        public List<WarehouseOrder> Orders { get; private set; }

        public WarehouseTaskManager TaskManager { get; private set; }

        public MonitorService Monitor { get; private set; }

        public Warehouse(string warehouseName)
        {
            if (string.IsNullOrWhiteSpace(warehouseName)) throw new ArgumentException("Warehouse name cannot be empty.");

            WarehouseName = warehouseName;
            Inventory = new Inventory();
            Orders = new List<WarehouseOrder>();
            TaskManager = new WarehouseTaskManager();
            Monitor = new MonitorService(this);
        }

        public void AddOrder(WarehouseOrder order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (Orders.Any(o => o.OrderID == order.OrderID)) throw new DuplicateOrderException("An order with ID " + order.OrderID + " already exists.");

            Orders.Add(order);
            WarehouseLogger.Log("Order added: " + order.OrderID);
        }

        public bool RemoveOrder(int orderID)
        {
            WarehouseOrder order = Orders.Find(o => o.OrderID == orderID);

            if (order == null) return false;

            Orders.Remove(order);
            WarehouseLogger.Log("Order removed: " + orderID);
            return true;
        }

        public WarehouseOrder GetOrderByID(int orderID)
        {
            return Orders.Find(o => o.OrderID == orderID);
        }

        public WarehouseTask CreateTask(string type, int workload, int orderAge, int resourceScarcity, int operationalRisk)
        {
            WarehouseTask task = TaskManager.CreateTask(type, workload, orderAge, resourceScarcity, operationalRisk);
            Monitor.RaiseTaskCreated(task);
            WarehouseLogger.Log("Task created: " + task.TaskID);
            return task;
        }

        public bool RemoveTask(int taskID)
        {
            WarehouseTask task = TaskManager.GetTaskByID(taskID);
            if (task == null) return false;

            bool removed = TaskManager.RemoveTask(taskID);
            if (removed)
            {
                Monitor.RaiseTaskRemoved(task);
                WarehouseLogger.Log("Task removed: " + taskID);
            }

            return removed;
        }

        public void StartMonitoring()
        {
            Monitor.Start();
        }

        public void StopMonitoring()
        {
            Monitor.Stop();
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
