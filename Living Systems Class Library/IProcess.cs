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
        bool TryGetMember(GetMemberBinder binder, out object result);
        bool TrySetMember(SetMemberBinder binder, object value);
    }

    public interface IProcessTemplate
    {

    }

    public interface IProcess
    {
        bool Execute();
        dynamic ExecuteArgs { get; set; }
        IProcessTemplate ProcessTemplate { get; set; }
        ISet<ProcessType> GetComponents();
    }
}
