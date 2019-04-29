using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    //TODO: make templates compliant with Decorator pattern too
    
    public class ReproducerProcessExecuteArgs : BasicProcessExecuteArgs
    {
        public LivingSystem system = null;
    }

    public class ReproducerProcessTemplate : BasicProcessTemplate
    {
        public Dictionary<string, IProcessTemplate> processesToAdd;
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
            ReproducerProcessExecuteArgs specificArgs = ExecuteArgs as ReproducerProcessExecuteArgs;
            bool result = _inner.Execute();
            if (result)
            {
                ReproducerProcessTemplate reproducerTemplate = ProcessTemplate as ReproducerProcessTemplate;
                specificArgs.system = new LivingSystem();
                if (reproducerTemplate != null && reproducerTemplate.processesToAdd != null)
                {
                    foreach (KeyValuePair<string, IProcessTemplate> process in reproducerTemplate.processesToAdd)
                    {
                        specificArgs.system.AddProcess(process.Key, process.Value);
                    }
                }
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
