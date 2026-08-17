using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1

{
    internal class Program
    {
        static void Main(string[] args)
        {
            WarehouseTaskManager manager =new WarehouseTaskManager();

            manager.CreateTask(
                "picking",
                workload: 6,
                orderAge: 9,
                resourceScarcity: 3,
                operationalRisk: 4);

            manager.CreateTask(
                "receiving",
                workload: 8,
                orderAge: 2,
                resourceScarcity: 5,
                operationalRisk: 6);

            manager.CreateTask(
                "restocking",
                workload: 4,
                orderAge: 1,
                resourceScarcity: 9,
                operationalRisk: 5);

            Console.WriteLine(
                "=== Tasks sorted by priority (highest first) ===\n");

            foreach (var task in manager.GetAllTasksByPriority())
            {
                Console.WriteLine(task);
            }


            //INVENTORY
            Inventory inventory = new Inventory();

            Product monitor = new Product(
                1,
                "Monitor",
                "Electronics",
                7.2,
                10,
                5);

            Product keyboard = new Product(
                2,
                "Keyboard",
                "Electronics",
                0.8,
                35,
                10);

            Product mouse = new Product(
                3,
                "Mouse",
                "Accessories",
                0.2,
                80,
                20);

            inventory.AddProduct(monitor);
            inventory.AddProduct(keyboard);
            inventory.AddProduct(mouse);

            Console.WriteLine("=== WAREHOUSE INVENTORY ===");

            foreach (Product product in inventory.GetAllProducts())
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();
            Console.WriteLine($"Total product types: {inventory.ProductCount}");

            Console.ReadLine();


            //WAREHOUDEORDER CLASS2==========
            WarehouseOrder order =
            new WarehouseOrder(101, "Lesego");

            order.AddProduct(monitor);
            order.AddProduct(keyboard);
            order.AddProduct(mouse); 



            //class9,warehouse object that stores everything 

            Warehouse warehouse=new Warehouse("WareSmart");
            warehouse.Inventory.AddProduct(monitor);
            warehouse.AddOrder(order);

            warehouse.TaskManager.CreateTask(
                "Picking",
                7,
                5,
                3,
                2);

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("=== EVENTS + THREADING DEMONSTRATION ===");
            Console.WriteLine(new string('=', 50) + "\n");

            // Create subscribers
            var consoleSub = new ConsoleSubscriber("UI");
            

            // Subscribe to events
            warehouse.Monitor.OnTaskCreated += consoleSub.OnTaskCreated;
            warehouse.Monitor.OnLowStock += consoleSub.OnLowStock;
            warehouse.Monitor.OnAlert += consoleSub.OnAlert;
           

            // Start background monitoring
            warehouse.StartMonitoring();
            Console.WriteLine("Monitoring service started on background thread\n");

            // Create more tasks (these will trigger events)
            Console.WriteLine("=== CREATING ADDITIONAL TASKS ===\n");
            warehouse.CreateTask("restocking", 9, 3, 8, 6);
            warehouse.CreateTask("picking", 5, 10, 2, 3);

            // Simulate stock changes (these will trigger low stock events)
            Console.WriteLine("\n=== SIMULATING STOCK CHANGES ===\n");

            Console.WriteLine("Removing stock from Monitor (10 → 2)...");
            monitor.RemoveStock(8); // Triggers low stock event

            Thread.Sleep(1000); // Give time for events to process

            Console.WriteLine("\nRemoving stock from Keyboard (35 → 5)...");
            keyboard.RemoveStock(30); // Triggers low stock event

            Thread.Sleep(1000);

            Console.WriteLine("\nRemoving stock from Mouse (80 → 5)...");
            mouse.RemoveStock(75); // Triggers low stock event

            // Restock (these will trigger restock alerts)
            Console.WriteLine("\n=== RESTOCKING ===\n");
            monitor.AddStock(20);
            keyboard.AddStock(15);
            mouse.AddStock(30);

            // Show current tasks after all changes
            Console.WriteLine("\n=== CURRENT TASKS (After changes) ===\n");
            foreach (var task in warehouse.TaskManager.GetAllTasksByPriority())
            {
                Console.WriteLine(task);
            }

            // Remove a task (triggers removal event)
            Console.WriteLine("\n=== REMOVING A TASK ===\n");
            var tasks = warehouse.TaskManager.GetAllTasksByPriority().ToList();
            if (tasks.Any())
            {
                int taskIdToRemove = tasks.First().TaskID;
                warehouse.RemoveTask(taskIdToRemove);
                Console.WriteLine($"Removed task #{taskIdToRemove}");
            }

            // Show final state
            Console.WriteLine("\n=== FINAL INVENTORY ===\n");
            foreach (Product product in warehouse.Inventory.GetAllProducts())
            {
                Console.WriteLine(product);
            }

            // Stop monitoring
            Console.WriteLine("\n=== SHUTTING DOWN ===\n");
            warehouse.StopMonitoring();
            Console.WriteLine("Monitoring service stopped");

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("DEMO COMPLETE - Press any key to exit");
            Console.ReadKey();

        }
    }
}
