using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaskNotification : EventArgs
    {
        public WarehouseTask Task { get; }
        public string Message { get; }
        public DateTime Time { get; }

        public TaskNotification(WarehouseTask task, string action)
        {
            Task = task;
            Message = $"{action}: {task.GetType().Name}";
            Time = DateTime.Now;
        }
    }
}
