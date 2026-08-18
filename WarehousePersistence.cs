using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    // Handles saving and loading the warehouse state.
    public static class WarehousePersistence
    {
        private const string SaveFile = "waresmart_save.txt";

        public static void Save(Warehouse warehouse)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(SaveFile, false))
                {
                    writer.WriteLine("WARESMART_SAVE");
                    writer.WriteLine("WAREHOUSE|" + warehouse.WarehouseName);

                    foreach (Product product in warehouse.Inventory.GetAllProducts())
                    {
                        writer.WriteLine("PRODUCT|" + product.ProductID + "|" + product.ProductName + "|" + product.Category + "|" + product.Weight + "|" + product.Quantity + "|" + product.ReorderLevel);
                    }

                    foreach (WarehouseOrder order in warehouse.Orders)
                    {
                        writer.WriteLine("ORDER|" + order.OrderID + "|" + order.CustomerName);
                        foreach (Product product in order.Products)
                        {
                            writer.WriteLine("ORDERPRODUCT|" + order.OrderID + "|" + product.ProductID);
                        }
                    }

                    foreach (WarehouseTask task in warehouse.TaskManager.GetAllTasksByPriority())
                    {
                        writer.WriteLine("TASK|" + task.GetType().Name + "|" + task.Workload + "|" + task.OrderAge + "|" + task.ResourceScarcity + "|" + task.OperationalRisk);
                    }
                }

                WarehouseLogger.Log("Warehouse saved successfully.");
                Console.WriteLine("Warehouse saved successfully.");
            }
            catch (Exception ex)
            {
                WarehouseLogger.Log("Save failed: " + ex.Message);
                throw new WarehousePersistenceException("Unable to save warehouse data.", ex);
            }
        }

        public static Warehouse Load()
        {
            try
            {
                if (!File.Exists(SaveFile)) throw new FileNotFoundException("Save file does not exist.");

                string[] lines = File.ReadAllLines(SaveFile);
                Warehouse warehouse = new Warehouse("WareSmart");
                Dictionary<int, WarehouseOrder> orders = new Dictionary<int, WarehouseOrder>();

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 0) continue;

                    switch (parts[0])
                    {
                        case "WAREHOUSE":
                            if (parts.Length >= 2) warehouse = new Warehouse(parts[1]);
                            break;

                        case "PRODUCT":
                            if (parts.Length >= 7)
                            {
                                Product product = new Product(int.Parse(parts[1]), parts[2], parts[3], double.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
                                warehouse.Inventory.AddProduct(product);
                            }
                            break;

                        case "ORDER":
                            if (parts.Length >= 3)
                            {
                                WarehouseOrder order = new WarehouseOrder(int.Parse(parts[1]), parts[2]);
                                warehouse.AddOrder(order);
                                orders.Add(order.OrderID, order);
                            }
                            break;

                        case "ORDERPRODUCT":
                            if (parts.Length >= 3 && orders.ContainsKey(int.Parse(parts[1])))
                            {
                                Product product = warehouse.Inventory.GetProductByID(int.Parse(parts[2]));
                                if (product != null) orders[int.Parse(parts[1])].AddProduct(product);
                            }
                            break;

                        case "TASK":
                            if (parts.Length >= 6)
                            {
                                string type = parts[1].Replace("Task", "").ToLower();
                                warehouse.TaskManager.CreateTask(type, int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                            }
                            break;
                    }
                }

                WarehouseLogger.Log("Warehouse loaded successfully.");
                return warehouse;
            }
            catch (Exception ex)
            {
                WarehouseLogger.Log("Load failed: " + ex.Message);
                throw new WarehousePersistenceException("Unable to load warehouse data.", ex);
            }
        }
    }
}
