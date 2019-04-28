using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    public class ProcessReproducerExecuteArgs : BasicProcessExecuteArgs
    {
        public LivingSystem system = null;
    }

    class ProcessReproducerDecorator: IProcess
    {
        private IProcess _inner;
        public IProcessExecuteArgs ExecuteArgs { get => this._inner.ExecuteArgs; set => this._inner.ExecuteArgs = value; }
        public IProcessTemplate ProcessTemplate { get => this._inner.ProcessTemplate; set => this._inner.ProcessTemplate = value; }

        public ProcessReproducerDecorator(IProcess inner)
        {
            this._inner = inner;
        }

        public bool Execute()
        {
            ProcessReproducerExecuteArgs specificArgs = ExecuteArgs as ProcessReproducerExecuteArgs;
            bool result = _inner.Execute();
            if (result)
            {
                specificArgs.system = new LivingSystem();
            }
            return result;
        }

        public ISet<ProcessType> GetComponents()
        {
            ISet<ProcessType> innerSet = _inner.GetComponents();
            innerSet.Add(ProcessType.REPRODUCER);
            return innerSet;
        }
    }
}
