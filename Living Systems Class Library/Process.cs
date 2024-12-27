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
        private IDictionary<string, object> properties = new Dictionary<string, object>();

        public ISet<ComponentType> ComponentTypes { get; set; } = new SortedSet<ComponentType>();

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (!IsMemberAllowed(binder.Name)) {
                result = null;
                return false;
            }
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

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (!IsMemberAllowed(binder.Name, value?.GetType()))
            {
                return false;
            }
            properties[binder.Name] = value!;
            return true;
        }

        private bool IsMemberAllowed(string name, Type? type = null)
        {
            if (name == "InputPile" && (type == null || type == typeof(MatterEnergyPile)))
            {
                return true;
            }
            if (name == "OutputPile" && (type == null || type == typeof(MatterEnergyPile)))
            {
                return true;
            }
            if (ComponentTypes.Contains(ComponentType.REPRODUCER) 
                && name == "System" && (type == null || type == typeof(LivingSystem)))
            {
                return true;
            }
            return false;
        }
    }

    public class BasicProcessTemplate : DynamicObject, IProcessTemplate
    {
        private IDictionary<string, object> properties = new Dictionary<string, object>();

        public ISet<ComponentType> ComponentTypes { get; set; } = new SortedSet<ComponentType>();

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (!IsMemberAllowed(binder.Name))
            {
                result = null;
                return false;
            }
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

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (!IsMemberAllowed(binder.Name, value?.GetType()))
            {
                return false;
            }
            properties[binder.Name] = value!;
            return true;
        }

        private bool IsMemberAllowed(string name, Type? type = null)
        {
            if (name == "Inputs" && (type == null || type == typeof(Dictionary<string, double>)))
            {
                return true;
            }
            if (name == "Outputs" && (type == null || type == typeof(Dictionary<string, double>)))
            {
                return true;
            }
            if (ComponentTypes.Contains(ComponentType.REPRODUCER)
                && name == "ProcessesToAdd" && (type == null || type == typeof(Dictionary<string, IProcessTemplate>)))
            {
                return true;
            }
            if (ComponentTypes.Contains(ComponentType.DECIDER)
                && name == "InitialValueRules" && (type == null || type == typeof(Dictionary<string, object>)))
            {
                return true;
            }
            return false;
        }
    }

    class Process : IProcess
    {
        LivingSystem system;
        public dynamic? ExecuteArgs { get; set; }
        public dynamic? ProcessTemplate { get; set; }

        public Process(LivingSystem system)
        {
            this.system = system;
        }
        public bool Execute()
        {
            dynamic? basicArgs = ExecuteArgs as BasicProcessExecuteArgs;
            dynamic? processTemplate = ProcessTemplate as BasicProcessTemplate;
            if (basicArgs == null || processTemplate == null) {
                return false;
            }
            if ((basicArgs!.InputPile == null && processTemplate!.Inputs.Count > 0) || !basicArgs.InputPile.RemoveBulk(processTemplate!.Inputs))
            {
                return false;
            }
            if (basicArgs.OutputPile == null && processTemplate!.Outputs.Count == 0)
            {
                return false;
            }
            basicArgs.OutputPile.AddBulk(processTemplate!.Outputs);
            return true;
        }

        public ISet<ComponentType> GetComponents()
        {
            return new SortedSet<ComponentType>();
        }
    }
}
