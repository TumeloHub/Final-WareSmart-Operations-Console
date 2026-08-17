using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaskDetail : EventArgs
    {
        public WarehouseTask Task { get; }
        public string Action { get; }
        public DateTime Time { get; }

        public TaskDetail(WarehouseTask task, string action)
        {
            Task = task;
            Action = action;
            Time = DateTime.Now;
        }
    }
}
