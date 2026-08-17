using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{
    // Owns creation / removal / querying of WarehouseTask entities.
    // This is the piece the menu will actually call into.
    public class WarehouseTaskManager
    {
        private List<WarehouseTask> tasks = new List<WarehouseTask>();
        private int nextTaskID = 1;

        public WarehouseTask CreateTask(string type, int workload, int orderAge, int resourceScarcity, int operationalRisk)
        {
            WarehouseTask task;

            switch (type.Trim().ToLower())
            {
                case "picking":
                    task = new PickingTask(nextTaskID, workload, orderAge, resourceScarcity, operationalRisk);
                    break;
                case "receiving":
                    task = new ReceivingTask(nextTaskID, workload, orderAge, resourceScarcity, operationalRisk);
                    break;
                case "restocking":
                    task = new RestockingTask(nextTaskID, workload, orderAge, resourceScarcity, operationalRisk);
                    break;
                default:
                    throw new ArgumentException($"Unknown task type: {type}");
            }

            tasks.Add(task);
            nextTaskID++;
            return task;
        }

        public bool RemoveTask(int taskID)
        {
            WarehouseTask task = tasks.FirstOrDefault(t => t.TaskID == taskID);
            if (task == null) return false;
            tasks.Remove(task);
            return true;
        }

        public WarehouseTask GetTaskByID(int taskID)
        {
            return tasks.FirstOrDefault(t => t.TaskID == taskID);
        }

        public IEnumerable<WarehouseTask> GetAllTasksByPriority()
        {
            return tasks.OrderByDescending(t => t.CalculatePriority());
        }

        public int Count => tasks.Count;
    }
}

