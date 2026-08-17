using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
            static void Main(string[] args)
            {
                // ==========================================
                // CREATE WAREHOUSE
                // ==========================================

                Warehouse warehouse = new Warehouse("WareSmart");


                // ==========================================
                // CREATE SOME STARTING PRODUCTS
                // ==========================================

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


                // Add starting products to inventory
                warehouse.Inventory.AddProduct(monitor);
                warehouse.Inventory.AddProduct(keyboard);
                warehouse.Inventory.AddProduct(mouse);


                // ==========================================
                // MAIN MENU
                // ==========================================

                bool running = true;

                while (running)
                {
                    Console.Clear();

                    Console.WriteLine("========================================");
                    Console.WriteLine("       WARESMART OPERATIONS CONSOLE");
                    Console.WriteLine("========================================");
                    Console.WriteLine();

                    Console.WriteLine("1. Inventory Management");
                    Console.WriteLine("2. Order Management");
                    Console.WriteLine("3. Task Management");
                    Console.WriteLine("4. Warehouse Summary");
                    Console.WriteLine("0. Exit");

                    Console.Write("\nSelect an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            InventoryMenu(warehouse);
                            break;

                        case "2":
                            OrderMenu(warehouse);
                            break;

                        case "3":
                            TaskMenu(warehouse);
                            break;

                        case "4":
                            WarehouseSummary(warehouse);
                            Console.ReadLine();
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("\nInvalid option.");
                            Console.WriteLine("Press Enter to try again...");
                            Console.ReadLine();
                            break;
                    }
                }

                Console.WriteLine("\nThank you for using WareSmart!");
            }


            static void InventoryMenu(Warehouse warehouse)
            {
                bool running = true;

                while (running)
                {
                    Console.Clear();

                    Console.WriteLine("========================================");
                    Console.WriteLine("          INVENTORY MANAGEMENT");
                    Console.WriteLine("========================================");
                    Console.WriteLine();

                    Console.WriteLine("1. View all products");
                    Console.WriteLine("2. Find product");
                    Console.WriteLine("3. Add product");
                    Console.WriteLine("4. Add stock");
                    Console.WriteLine("5. Remove stock");
                    Console.WriteLine("6. Delete product");
                    Console.WriteLine("0. Back to main menu");

                    Console.Write("\nSelect an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ViewProducts(warehouse);
                            break;

                        case "2":
                            FindProduct(warehouse);
                            break;

                        case "3":
                            AddProduct(warehouse);
                            break;

                        case "4":
                            AddStock(warehouse);
                            break;

                        case "5":
                            RemoveStock(warehouse);
                            break;

                        case "6":
                            DeleteProduct(warehouse);
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("\nInvalid option.");
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            //method for order menu
            static void OrderMenu(Warehouse warehouse)
            {
                bool running = true;

                while (running)
                {
                    Console.Clear();

                    Console.WriteLine("========================================");
                    Console.WriteLine("           ORDER MANAGEMENT");
                    Console.WriteLine("========================================");
                    Console.WriteLine();

                    Console.WriteLine("1. View all orders");
                    Console.WriteLine("2. Find order");
                    Console.WriteLine("3. Create order");
                    Console.WriteLine("4. Add product to order");
                    Console.WriteLine("5. Remove product from order");
                    Console.WriteLine("6. Delete order");
                    Console.WriteLine("0. Back to main menu");

                    Console.Write("\nSelect an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ViewOrders(warehouse);
                            break;

                        case "2":
                            FindOrder(warehouse);
                            break;

                        case "3":
                            CreateOrder(warehouse);
                            break;

                        case "4":
                            AddProductToOrder(warehouse);
                            break;

                        case "5":
                            RemoveProductFromOrder(warehouse);
                            break;

                        case "6":
                            DeleteOrder(warehouse);
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("\nInvalid option.");
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            //FUNCTIONS FOR ORDER MENU 
            static void ViewOrders(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              ALL ORDERS");
                Console.WriteLine("========================================");
                Console.WriteLine();

                if (warehouse.Orders.Count == 0)
                {
                    Console.WriteLine("There are no orders.");
                }
                else
                {
                    foreach (WarehouseOrder order in warehouse.Orders)
                    {
                        Console.WriteLine(order);
                    }
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            static void FindOrder(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("               FIND ORDER");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Order ID: ");

                int orderID = int.Parse(Console.ReadLine());

                WarehouseOrder order = warehouse.GetOrderByID(orderID);

                if (order == null)
                {
                    Console.WriteLine("\nOrder not found.");
                }
                else
                {
                    Console.WriteLine("\nOrder found:");
                    Console.WriteLine(order);

                    Console.WriteLine("\nProducts in this order:");

                    foreach (Product product in order.Products)
                    {
                        Console.WriteLine(product);
                    }
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            static void CreateOrder(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              CREATE ORDER");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Order ID: ");
                int orderID = int.Parse(Console.ReadLine());

                Console.Write("Enter customer name: ");
                string customerName = Console.ReadLine();

                WarehouseOrder order =
                    new WarehouseOrder(orderID, customerName);

                warehouse.AddOrder(order);

                Console.WriteLine("\nOrder successfully created!");
                Console.WriteLine(order);

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //ADDING PRODUCT TO AN ORDER 
            static void AddProductToOrder(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("        ADD PRODUCT TO ORDER");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Order ID: ");
                int orderID = int.Parse(Console.ReadLine());

                WarehouseOrder order = warehouse.GetOrderByID(orderID);

                if (order == null)
                {
                    Console.WriteLine("\nOrder not found.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Product ID: ");
                int productID = int.Parse(Console.ReadLine());

                Product product = warehouse.Inventory.GetProductByID(productID);

                if (product == null)
                {
                    Console.WriteLine("\nProduct not found in inventory.");
                    Console.ReadLine();
                    return;
                }

                order.AddProduct(product);

                Console.WriteLine("\nProduct successfully added to order!");
                Console.WriteLine(order);

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //REMOVE PRODUCT FROM ORDER 
            static void RemoveProductFromOrder(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("       REMOVE PRODUCT FROM ORDER");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Order ID: ");
                int orderID = int.Parse(Console.ReadLine());

                WarehouseOrder order = warehouse.GetOrderByID(orderID);

                if (order == null)
                {
                    Console.WriteLine("\nOrder not found.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Product ID: ");
                int productID = int.Parse(Console.ReadLine());

                bool removed = order.RemoveProduct(productID);

                if (removed)
                {
                    Console.WriteLine("\nProduct removed from order.");
                }
                else
                {
                    Console.WriteLine("\nProduct was not found in this order.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //DELETE ORDER FROM PRODUCT 
            static void DeleteOrder(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              DELETE ORDER");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Order ID: ");
                int orderID = int.Parse(Console.ReadLine());

                bool removed = warehouse.RemoveOrder(orderID);

                if (removed)
                {
                    Console.WriteLine("\nOrder successfully deleted.");
                }
                else
                {
                    Console.WriteLine("\nOrder not found.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            //task management menu
            static void TaskMenu(Warehouse warehouse)
            {
                bool running = true;

                while (running)
                {
                    Console.Clear();

                    Console.WriteLine("========================================");
                    Console.WriteLine("             TASK MANAGEMENT");
                    Console.WriteLine("========================================");
                    Console.WriteLine();

                    Console.WriteLine("1. View all tasks");
                    Console.WriteLine("2. View tasks by priority");
                    Console.WriteLine("3. Create task");
                    Console.WriteLine("4. Delete task");
                    Console.WriteLine("0. Back to main menu");

                    Console.Write("\nSelect an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ViewTasks(warehouse);
                            break;

                        case "2":
                            ViewTasksByPriority(warehouse);
                            break;

                        case "3":
                            CreateTask(warehouse);
                            break;

                        case "4":
                            DeleteTask(warehouse);
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("\nInvalid option.");
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            static void ViewTasks(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("               ALL TASKS");
                Console.WriteLine("========================================");
                Console.WriteLine();

                if (warehouse.TaskManager.Count == 0)
                {
                    Console.WriteLine("There are no tasks.");
                }
                else
                {
                    foreach (WarehouseTask task in warehouse.TaskManager.GetAllTasksByPriority())
                    {
                        Console.WriteLine(task);
                    }
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            //VIEW TASKS BY PRIORITY
            static void ViewTasksByPriority(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("          TASKS BY PRIORITY");
                Console.WriteLine("========================================");
                Console.WriteLine();

                if (warehouse.TaskManager.Count == 0)
                {
                    Console.WriteLine("There are no tasks.");
                }
                else
                {
                    foreach (WarehouseTask task in warehouse.TaskManager.GetAllTasksByPriority())
                    {
                        Console.WriteLine(task);
                    }
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //creating task 
            static void CreateTask(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              CREATE TASK");
                Console.WriteLine("========================================");

                Console.WriteLine();
                Console.WriteLine("Task Types:");
                Console.WriteLine("1. Picking");
                Console.WriteLine("2. Receiving");
                Console.WriteLine("3. Restocking");

                Console.Write("\nSelect task type: ");

                string choice = Console.ReadLine();

                string type;

                switch (choice)
                {
                    case "1":
                        type = "picking";
                        break;

                    case "2":
                        type = "receiving";
                        break;

                    case "3":
                        type = "restocking";
                        break;

                    default:
                        Console.WriteLine("\nInvalid task type.");
                        Console.ReadLine();
                        return;
                }

                Console.Write("\nWorkload (0-10): ");
                int workload = int.Parse(Console.ReadLine());

                Console.Write("Order age (0-10): ");
                int orderAge = int.Parse(Console.ReadLine());

                Console.Write("Resource scarcity (0-10): ");
                int resourceScarcity = int.Parse(Console.ReadLine());

                Console.Write("Operational risk (0-10): ");
                int operationalRisk = int.Parse(Console.ReadLine());

                WarehouseTask task = warehouse.TaskManager.CreateTask(
                    type,
                    workload,
                    orderAge,
                    resourceScarcity,
                    operationalRisk);

                Console.WriteLine("\nTask successfully created!");
                Console.WriteLine(task);

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //delete task
            static void DeleteTask(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              DELETE TASK");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Task ID: ");

                int taskID = int.Parse(Console.ReadLine());

                bool removed = warehouse.TaskManager.RemoveTask(taskID);

                if (removed)
                {
                    Console.WriteLine("\nTask successfully deleted.");
                }
                else
                {
                    Console.WriteLine("\nTask not found.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }


            //VIEW PERODUCTS 
            static void ViewProducts(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("             ALL PRODUCTS");
                Console.WriteLine("========================================");
                Console.WriteLine();

                if (warehouse.Inventory.ProductCount == 0)
                {
                    Console.WriteLine("There are no products in the inventory.");
                }
                else
                {
                    foreach (Product product in warehouse.Inventory.GetAllProducts())
                    {
                        Console.WriteLine(product);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to return...");
                Console.ReadLine();
            }

            //FIND PRODUCT 
            static void FindProduct(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("             FIND PRODUCT");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Product ID: ");

                int productID;

                if (!int.TryParse(Console.ReadLine(), out productID))
                {
                    Console.WriteLine("\nPlease enter a valid number.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Product product = warehouse.Inventory.GetProductByID(productID);

                if (product == null)
                {
                    Console.WriteLine("\nProduct not found.");
                }
                else
                {
                    Console.WriteLine("\nProduct found:");
                    Console.WriteLine(product);
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //CREte productss

            static void AddProduct(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              ADD PRODUCT");
                Console.WriteLine("========================================");

                Console.Write("\nProduct ID: ");
                int productID = int.Parse(Console.ReadLine());

                Console.Write("Product name: ");
                string productName = Console.ReadLine();

                Console.Write("Category: ");
                string category = Console.ReadLine();

                Console.Write("Weight (kg): ");
                double weight = double.Parse(Console.ReadLine());

                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Reorder level: ");
                int reorderLevel = int.Parse(Console.ReadLine());

                Product product = new Product(
                    productID,
                    productName,
                    category,
                    weight,
                    quantity,
                    reorderLevel);

                warehouse.Inventory.AddProduct(product);

                Console.WriteLine("\nProduct successfully added!");
                Console.WriteLine(product);

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            //add stock
            static void AddStock(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              ADD STOCK");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Product ID: ");
                int productID = int.Parse(Console.ReadLine());

                Product product = warehouse.Inventory.GetProductByID(productID);

                if (product == null)
                {
                    Console.WriteLine("\nProduct not found.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter amount to add: ");
                int amount = int.Parse(Console.ReadLine());

                product.AddStock(amount);

                Console.WriteLine("\nStock successfully updated!");
                Console.WriteLine(product);

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            //remove stock              
            static void RemoveStock(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("             REMOVE STOCK");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Product ID: ");
                int productID = int.Parse(Console.ReadLine());

                Product product = warehouse.Inventory.GetProductByID(productID);

                if (product == null)
                {
                    Console.WriteLine("\nProduct not found.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter amount to remove: ");
                int amount = int.Parse(Console.ReadLine());

                bool success = product.RemoveStock(amount);

                if (success)
                {
                    Console.WriteLine("\nStock successfully removed!");
                    Console.WriteLine(product);
                }
                else
                {
                    Console.WriteLine("\nNot enough stock available.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            //delete product 
            static void DeleteProduct(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("             DELETE PRODUCT");
                Console.WriteLine("========================================");

                Console.Write("\nEnter Product ID: ");
                int productID = int.Parse(Console.ReadLine());

                bool removed = warehouse.Inventory.RemoveProduct(productID);

                if (removed)
                {
                    Console.WriteLine("\nProduct successfully deleted.");
                }
                else
                {
                    Console.WriteLine("\nProduct not found.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            //METHOD FOR WAREHOUSE SUMMARY 
            static void WarehouseSummary(Warehouse warehouse)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("          WAREHOUSE SUMMARY");
                Console.WriteLine("========================================");
                Console.WriteLine();

                Console.WriteLine(warehouse);

                Console.WriteLine();
                Console.WriteLine("------------- TASK PRIORITIES ----------");

                if (warehouse.TaskManager.Count == 0)
                {
                    Console.WriteLine("No tasks currently available.");
                }
                else
                {
                    foreach (WarehouseTask task in warehouse.TaskManager.GetAllTasksByPriority())
                    {
                        Console.WriteLine(task);
                    }
                }

                Console.WriteLine();

                Console.WriteLine("------------- ORDERS -------------------");

                if (warehouse.Orders.Count == 0)
                {
                    Console.WriteLine("No orders currently available.");
                }
                else
                {
                    foreach (WarehouseOrder order in warehouse.Orders)
                    {
                        Console.WriteLine(order);
                    }
                }

                Console.WriteLine("\nPress Enter to return...");

                Console.ReadLine();
            }
        

    }


}
