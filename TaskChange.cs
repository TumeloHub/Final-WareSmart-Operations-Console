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

        public TaskChange(WarehouseTask _task, string _changeType)
        {
            Task = _task;
            ChangeType = _changeType;
            Time = DateTime.Now;
        }
    }
}
