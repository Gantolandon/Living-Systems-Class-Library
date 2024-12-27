using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public interface IProcessExecuteArgs
    {
        ISet<ComponentType> ComponentTypes { get; set; }
    }

    public interface IProcessTemplate
    {
    }

    public interface IProcess
    {
        bool Execute();
        dynamic? ExecuteArgs { get; set; }
        dynamic? ProcessTemplate { get; set; }
        ISet<ComponentType> GetComponents();
    }
}
