using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public interface IProcessExecuteArgs
    {

    }

    public interface IProcessTemplate
    {

    }

    public interface IProcess
    {
        bool Execute();
        IProcessExecuteArgs ExecuteArgs { get; set; }
        IProcessTemplate ProcessTemplate { get; set; }
        ISet<ProcessType> GetComponents();
    }
}
