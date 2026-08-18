using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaskArgs : EventArgs
    {
        public WarehouseTask Task { get; }
        public string Action { get; }
        public DateTime Time { get; }

        public TaskArgs(WarehouseTask _task, string _action)
        {
            Task = _task;
            Action = _action;
            Time = DateTime.Now;
        }
    }
}
