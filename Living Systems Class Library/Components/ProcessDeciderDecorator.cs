using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    class ProcessDeciderDecorator : IProcess
    {
        private IProcess _inner;
        public dynamic ExecuteArgs { get => this._inner.ExecuteArgs; set => this._inner.ExecuteArgs = value; }
        public dynamic ProcessTemplate { get => this._inner.ProcessTemplate; set => this._inner.ProcessTemplate = value; }

        private readonly IDictionary<string, Value> values;

        public ProcessDeciderDecorator(IProcess inner)
        {
            this._inner = inner;
            this.values = new Dictionary<string, Value>();
            IDictionary<string, double> valueRules = this.ProcessTemplate.InitialValueRules;
            this.PopulateValues(valueRules);
        }

        private void PopulateValues(IDictionary<string, double> initialValueRules)
        {
            foreach (KeyValuePair<string, double> pair in initialValueRules)
            {
                this.values[pair.Key] = new Value(pair.Key, pair.Value);
            }
        }

        public Value? SelectValueToResolve()
        {
            Value? selectedValue = null;
            double previousEvaluateWithPriority = 0.0d;
            foreach (KeyValuePair<string, Value> value in values)
            {
                double currentEvaluateWithPriority = value.Value.EvaluateWithPriority();
                if (currentEvaluateWithPriority > previousEvaluateWithPriority)
                {
                    selectedValue = value.Value;
                    previousEvaluateWithPriority = currentEvaluateWithPriority;
                }
            }
            return selectedValue;
        }

        public bool Execute()
        {
            Value? currentValue = null;
            currentValue = SelectValueToResolve();
            return _inner.Execute();
        }

        public ISet<ComponentType> GetComponents()
        {
            ISet<ComponentType> innerSet = _inner.GetComponents();
            innerSet.Add(ComponentType.DECIDER);
            return innerSet;
        }
    }
}
