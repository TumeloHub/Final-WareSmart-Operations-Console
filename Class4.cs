using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{
    // Restocking exists to fix low stock, so ResourceScarcity dominates
    // the weighting - a nearly-empty shelf outranks almost everything else.
    public class RestockingTask : WarehouseTask
    {
        private const double WeightResourceScarcity = 0.45;
        private const double WeightOperationalRisk = 0.25;
        private const double WeightWorkload = 0.20;
        private const double WeightOrderAge = 0.10;

        public RestockingTask(int _taskID, int _workload, int _orderAge, int _resourceScarcity, int _operationalRisk)
            : base(_taskID, _workload, _orderAge, _resourceScarcity, _operationalRisk)
        {
        }

        public override double CalculatePriority()
        {
            double weighted =
                (ResourceScarcity * WeightResourceScarcity) +
                (OperationalRisk * WeightOperationalRisk) +
                (Workload * WeightWorkload) +
                (OrderAge * WeightOrderAge);

            return Math.Round(weighted * 10, 1);
        }
    }

}
