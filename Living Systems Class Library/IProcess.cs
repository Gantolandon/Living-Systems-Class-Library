using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    interface IProcess
    {
        IDictionary<string, double> Inputs { get; set; }
        IDictionary<string, double> Outputs { get; set; }

        bool Execute();
        bool Execute(MatterEnergyPile input, MatterEnergyPile output);
        ISet<string> GetComponents();
    }
}
