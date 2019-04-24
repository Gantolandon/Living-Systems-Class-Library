using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public class BasicProcessExecuteArgs : IProcessExecuteArgs
    {
        public MatterEnergyPile inputPile;
        public MatterEnergyPile outputPile;
    }

    class Process : IProcess
    {
        IDictionary<string, double> inputs;
        IDictionary<string, double> outputs;

        LivingSystem system;

        public IDictionary<string, double> Inputs { get => inputs; set => inputs = new Dictionary<string, double>(value); }
        public IDictionary<string, double> Outputs { get => outputs; set => outputs = new Dictionary<string, double>(value); }
   
        public Process(LivingSystem system)
        {
            this.system = system;
        }
        public bool Execute(IProcessExecuteArgs args)
        {
            BasicProcessExecuteArgs basicArgs = args as BasicProcessExecuteArgs;
            if ((basicArgs.inputPile == null && Inputs.Count > 0) || !basicArgs.inputPile.RemoveBulk(inputs))
            {
                return false;
            }
            if (basicArgs.outputPile == null && Outputs.Count == 0)
            {
                return false;
            }
            basicArgs.outputPile.AddBulk(outputs);
            return true;
        }

        public ISet<string> GetComponents()
        {
            return new SortedSet<string>();
        }
    }
}
