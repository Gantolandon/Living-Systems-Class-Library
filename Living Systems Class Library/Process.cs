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

    public class BasicProcessTemplate : IProcessTemplate
    {
        public IDictionary<string, double> inputs;
        public IDictionary<string, double> outputs;
    }

    class Process : IProcess
    {
        LivingSystem system;
        public IProcessExecuteArgs ExecuteArgs { get; set; }
        public IProcessTemplate ProcessTemplate { get; set; } 

        public Process(LivingSystem system)
        {
            this.system = system;
        }
        public bool Execute()
        {
            BasicProcessExecuteArgs basicArgs = ExecuteArgs as BasicProcessExecuteArgs;
            BasicProcessTemplate processTemplate = ProcessTemplate as BasicProcessTemplate;
            if ((basicArgs.inputPile == null && processTemplate.inputs.Count > 0) || !basicArgs.inputPile.RemoveBulk(processTemplate.inputs))
            {
                return false;
            }
            if (basicArgs.outputPile == null && processTemplate.outputs.Count == 0)
            {
                return false;
            }
            basicArgs.outputPile.AddBulk(processTemplate.outputs);
            return true;
        }

        public ISet<ProcessType> GetComponents()
        {
            return new SortedSet<ProcessType>();
        }
    }
}
