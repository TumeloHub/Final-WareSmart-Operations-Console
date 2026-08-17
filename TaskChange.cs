using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaskChange : EventArgs
    {
        public WarehouseTask Task { get; }
        public string ChangeType { get; } // Created, Removed, etc.
        public DateTime Time { get; }

        public TaskChange(WarehouseTask task, string changeType)
        {
            Task = task;
            ChangeType = changeType;
            Time = DateTime.Now;
        }
    }
}
