using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ConsoleSubscriber
    {
        private string name;

        public ConsoleSubscriber(string name)
        {
            this.name = name;
        }

        public void OnTaskCreated(object sender, TaskInfo e)
        {
            Console.WriteLine($"[{name}] {e.Action}: {e.Task.GetType().Name} (Priority: {e.Task.GetPriorityLevel()})");
        }

        public void OnLowStock(object sender, StockInfo e)
        {
            Console.WriteLine($"[{name}] {e.Product.ProductName}: {e.OldStock} → {e.NewStock}");
        }

        public void OnTaskRemoved(object sender, TaskInfo e)
        {
            Console.WriteLine($"[{name}] {e.Action}: {e.Task.GetType().Name} (Priority: {e.Task.GetPriorityLevel()})");
        }

        public void OnAlert(object sender, string message)
        {
            Console.WriteLine($"[{name}] {message}");
        }
    }
}

