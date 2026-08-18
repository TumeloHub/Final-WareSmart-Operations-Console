using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{
    // Receiving is about clearing incoming volume before it backs up the
    // dock, so Workload dominates the weighting.
    public class ReceivingTask : WarehouseTask
    {
        private const double WeightWorkload = 0.40;
        private const double WeightOperationalRisk = 0.25;
        private const double WeightResourceScarcity = 0.20;
        private const double WeightOrderAge = 0.15;

        public ReceivingTask(int _taskID, int _workload, int _orderAge, int _resourceScarcity, int _operationalRisk)
            : base(_taskID, _workload, _orderAge, _resourceScarcity, _operationalRisk)
        {
        }

        public override double CalculatePriority()
        {
            double weighted =
                (Workload * WeightWorkload) +
                (OperationalRisk * WeightOperationalRisk) +
                (ResourceScarcity * WeightResourceScarcity) +
                (OrderAge * WeightOrderAge);

            return Math.Round(weighted * 10, 1);
        }
    }
}
