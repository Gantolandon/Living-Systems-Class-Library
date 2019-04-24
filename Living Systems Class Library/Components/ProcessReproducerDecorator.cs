using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    class ProcessReproducerExecuteArgs : BasicProcessExecuteArgs
    {
        public LivingSystem system = null;
    }

    class ProcessReproducerDecorator: IProcess
    {
        private IProcess _inner;

        public ProcessReproducerDecorator(IProcess inner)
        {
            this._inner = inner;
        }

        public bool Execute(IProcessExecuteArgs args)
        {
            ProcessReproducerExecuteArgs specificArgs = args as ProcessReproducerExecuteArgs;
            specificArgs.system = new LivingSystem();
            bool result = _inner.Execute(args);
            return result;
        }

        public ISet<string> GetComponents()
        {
            ISet<string> innerSet = _inner.GetComponents();
            innerSet.Add("REPRODUCER");
            return innerSet;
        }
    }
}
