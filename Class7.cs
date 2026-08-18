using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{
    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    // Abstract base = every warehouse task shares an identity and four rating
    // factors, but HOW those factors turn into a priority score is left to
    // each derived class (see CalculatePriority in Picking/Receiving/Restocking).
    public abstract class WarehouseTask : IPrioritizable
    {
        public int TaskID { get; private set; }

        // Each factor is rated 0-10. Private setters mean nothing outside this
        // class can quietly overwrite them without going through validation.
        public int Workload { get; private set; }
        public int OrderAge { get; private set; }
        public int ResourceScarcity { get; private set; }
        public int OperationalRisk { get; private set; }

        protected WarehouseTask(int taskID, int workload, int orderAge, int resourceScarcity, int operationalRisk)
        {
            TaskID = taskID;
            Workload = Validate(workload, nameof(workload));
            OrderAge = Validate(orderAge, nameof(orderAge));
            ResourceScarcity = Validate(resourceScarcity, nameof(resourceScarcity));
            OperationalRisk = Validate(operationalRisk, nameof(operationalRisk));
        }

        private int Validate(int value, string fieldName)
        {
            if (value < 0 || value > 10)
                throw new ArgumentOutOfRangeException(fieldName, $"{fieldName} must be rated between 0 and 10.");
            return value;
        }

        // POLYMORPHISM: every derived class weighs these four factors
        // differently because the domain reason for urgency is different
        // for picking vs receiving vs restocking.
        public abstract double CalculatePriority();

        // Shared domain rule: the same thresholds apply no matter which
        // task type produced the score.
        public PriorityLevel GetPriorityLevel()
        {
            double score = CalculatePriority();
            if (score >= 85) return PriorityLevel.Critical;
            if (score >= 65) return PriorityLevel.High;
            if (score >= 40) return PriorityLevel.Medium;
            return PriorityLevel.Low;
        }

        public override string ToString()
        {
            return $"[Task #{TaskID}] {GetType().Name,-14} | Priority: {CalculatePriority(),5:F1}% ({GetPriorityLevel()})";
        }
    }
}
