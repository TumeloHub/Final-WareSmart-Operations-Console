using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MonitorService : EventArgs
    {
        public event Action<object, TaskInfo> OnTaskCreated;
        public event Action<object, TaskInfo> OnTaskRemoved;
        public event Action<object, StockInfo> OnLowStock;
        public event Action<object, string> OnAlert;

        private Warehouse warehouse;
        private bool isRunning = false;
        private Thread monitorThread;
        private Dictionary<int, int> lastStock = new Dictionary<int, int>();

        public MonitorService(Warehouse warehouse)
        {
            this.warehouse = warehouse;
        }

        public void Start()
        {
            if (isRunning) return;
            isRunning = true;
            monitorThread = new Thread(MonitorLoop);
            monitorThread.IsBackground = true;
            monitorThread.Start();
            OnAlert?.Invoke(this, "Monitoring Started");
        }

        public void Stop()
        {
            isRunning = false;
            OnAlert?.Invoke(this, "Monitoring Stopped");
        }

        private void MonitorLoop()
        {
            while (isRunning)
            {
                CheckStockLevels();
                Thread.Sleep(3000);
            }
        }

        private void CheckStockLevels()
        {
            foreach (var product in warehouse.Inventory.GetAllProducts())
            {
                int currentStock = product.Quantity;

                if (!lastStock.ContainsKey(product.ProductID))
                {
                    lastStock[product.ProductID] = currentStock;
                    continue;
                }

                int previousStock = lastStock[product.ProductID];

                if (currentStock < previousStock)
                {
                    lastStock[product.ProductID] = currentStock;

                    if (product.NeedsRestocking())
                    {
                        OnLowStock?.Invoke(this, new StockInfo(product, previousStock, currentStock));
                        OnAlert?.Invoke(this, $"LOW STOCK: {product.ProductName} has {currentStock} left!");
                    }
                }
                else if (currentStock > previousStock)
                {
                    lastStock[product.ProductID] = currentStock;
                    OnAlert?.Invoke(this, $"RESTOCKED: {product.ProductName} now has {currentStock}");
                }
            }
        }

        public void RaiseTaskCreated(WarehouseTask task)
        {
            OnTaskCreated?.Invoke(this, new TaskInfo(task, "Created"));

            if (task.GetPriorityLevel() == PriorityLevel.Critical)
            {
                OnAlert?.Invoke(this, $"CRITICAL TASK: {task.GetType().Name} #{task.TaskID}");
            }
        }
        public void RaiseTaskRemoved(WarehouseTask task)
        {
            OnTaskRemoved?.Invoke(this, new TaskInfo(task, "Removed"));
        }
    }
}
    

