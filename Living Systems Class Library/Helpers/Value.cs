using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Helpers
{
    class Value
    {
        public string Id { get; set; }
        public double CurrentValue { get; set; }
        public double TargetValue { get; set; }

        public double PriorityMultiplier { get; set; } = 1.0d;

        public Value(string id, double targetValue, double currentValue = 0.0d)
        {
            this.Id = id;
            this.TargetValue = targetValue;
            this.CurrentValue = currentValue;
        }

        public double EvaluateWithPriority()
        {
            return (this.Evaluate() * this.PriorityMultiplier);
        }

        public double Evaluate()
        {
            return (this.CurrentValue - this.TargetValue);
        }
    }
}
