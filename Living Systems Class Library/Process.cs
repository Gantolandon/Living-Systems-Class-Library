using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public class BasicProcessExecuteArgs : DynamicObject, IProcessExecuteArgs
    {
        public IDictionary<string, object> properties = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (properties.ContainsKey(binder.Name))
            {
                result = properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return true;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            properties[binder.Name] = value;
            return true;
        }
    }

    public class BasicProcessTemplate : IProcessTemplate
    {
        public ProcessType type;
        public IDictionary<string, double> inputs;
        public IDictionary<string, double> outputs;
    }

    class Process : IProcess
    {
        LivingSystem system;
        public dynamic ExecuteArgs { get; set; }
        public IProcessTemplate ProcessTemplate { get; set; } 

        public Process(LivingSystem system)
        {
            this.system = system;
        }
        public bool Execute()
        {
            dynamic basicArgs = ExecuteArgs as BasicProcessExecuteArgs;
            BasicProcessTemplate processTemplate = ProcessTemplate as BasicProcessTemplate;
            if ((basicArgs.InputPile == null && processTemplate.inputs.Count > 0) || !basicArgs.InputPile.RemoveBulk(processTemplate.inputs))
            {
                return false;
            }
            if (basicArgs.OutputPile == null && processTemplate.outputs.Count == 0)
            {
                return false;
            }
            basicArgs.OutputPile.AddBulk(processTemplate.outputs);
            return true;
        }

        public ISet<ProcessType> GetComponents()
        {
            return new SortedSet<ProcessType>();
        }
    }
}
