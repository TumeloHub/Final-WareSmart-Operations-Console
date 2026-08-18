using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{

     
    // Picking is customer-facing: the older the order sits unpicked, the
    // worse the SLA breach, so OrderAge dominates the weighting.
    public class PickingTask : WarehouseTask
    {
        private const double WeightOrderAge = 0.40;
        private const double WeightWorkload = 0.25;
        private const double WeightOperationalRisk = 0.20;
        private const double WeightResourceScarcity = 0.15;

        public PickingTask(int _taskID, int _workload, int _orderAge, int _resourceScarcity, int _operationalRisk)
            : base(_taskID, _workload, _orderAge, _resourceScarcity, _operationalRisk)
        {
        }

        public override double CalculatePriority()
        {
            double weighted =
                (OrderAge * WeightOrderAge) +
                (Workload * WeightWorkload) +
                (OperationalRisk * WeightOperationalRisk) +
                (ResourceScarcity * WeightResourceScarcity);

            return Math.Round(weighted * 10, 1); // factors are 0-10, so *10 gives a 0-100% score
        }
    }
}
