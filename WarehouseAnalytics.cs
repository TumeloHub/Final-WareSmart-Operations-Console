using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    // Provides analytical reports using LINQ.
    public static class WarehouseAnalytics
    {
        public static IEnumerable<Product> GetProductsNeedingRestock(Warehouse warehouse)
        {
            return warehouse.Inventory.GetAllProducts().Where(p => p.NeedsRestocking());
        }

        public static int GetTotalStock(Warehouse warehouse)
        {
            return warehouse.Inventory.GetAllProducts().Sum(p => p.Quantity);
        }

        public static Product GetHeaviestProduct(Warehouse warehouse)
        {
            return warehouse.Inventory.GetAllProducts().OrderByDescending(p => p.Weight).FirstOrDefault();
        }

        public static WarehouseTask GetHighestPriorityTask(Warehouse warehouse)
        {
            return warehouse.TaskManager.GetAllTasksByPriority().FirstOrDefault();
        }

        public static void DisplayReport(Warehouse warehouse)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("          WAREHOUSE ANALYTICS");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("Total stock units: " + GetTotalStock(warehouse));
                Console.WriteLine("Number of products: " + warehouse.Inventory.ProductCount);
                Console.WriteLine("Number of orders: " + warehouse.Orders.Count);
                Console.WriteLine("Number of tasks: " + warehouse.TaskManager.Count);

                Product heaviest = GetHeaviestProduct(warehouse);
                if (heaviest != null) 
                {
                    Console.WriteLine("Heaviest product: " + heaviest.ProductName + " (" + heaviest.Weight.ToString("F1") + "kg)");
                }

                Console.WriteLine();
                Console.WriteLine("Products needing restock:");

                IEnumerable<Product> restock = GetProductsNeedingRestock(warehouse);
                if (!restock.Any())
                {
                    Console.WriteLine("No products currently need restocking.");
                }
                else
                {
                    foreach (Product product in restock)
                    {
                        Console.WriteLine("- " + product.ProductName + " | Stock: " + product.Quantity + " | Reorder Level: " + product.ReorderLevel);
                    }
                }

                WarehouseTask highestPriority = GetHighestPriorityTask(warehouse);
                Console.WriteLine();
                if (highestPriority != null)
                {
                    Console.WriteLine("Highest priority task: " + highestPriority);
                }

                else 
                { 
                    Console.WriteLine("No tasks currently available."); 
                }
            }
            catch (Exception ex)
            {
                WarehouseLogger.Log("Analytics error: " + ex.Message);
                Console.WriteLine("Unable to generate analytics: " + ex.Message);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Analytics operation completed.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
