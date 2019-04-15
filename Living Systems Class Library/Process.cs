using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
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

        public bool Execute()
        {
            return true;
        }

        public bool Execute(MatterEnergyPile inputPile, MatterEnergyPile outputPile)
        {
            if (!inputPile.RemoveBulk(inputs))
            {
                return false;
            }
            outputPile.AddBulk(outputs);
            return true;
        }

        public ISet<string> GetComponents()
        {
            return new SortedSet<string>();
        }
    }
}
