﻿using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    //TODO: make templates compliant with Decorator pattern too

    class ProcessReproducerDecorator: IProcess
    {
        private IProcess _inner;
        public dynamic? ExecuteArgs { get => this._inner.ExecuteArgs; set => this._inner.ExecuteArgs = value; }
        public dynamic? ProcessTemplate { get => this._inner.ProcessTemplate; set => this._inner.ProcessTemplate = value; }

        public ProcessReproducerDecorator(IProcess inner)
        {
            this._inner = inner;
        }

        public bool Execute()
        {
            if (ExecuteArgs is null) {
                return false;
            }
            dynamic specificArgs = ExecuteArgs;
            bool result = _inner.Execute();
            if (result)
            {
                dynamic? reproducerTemplate = ProcessTemplate;
                specificArgs.System = new LivingSystem();
                if (reproducerTemplate != null && reproducerTemplate!.ProcessesToAdd != null)
                {
                    foreach (KeyValuePair<string, IProcessTemplate> process in reproducerTemplate!.ProcessesToAdd)
                    {
                        specificArgs.System.AddProcess(process.Key, process.Value);
                    }
                }
            }
            return result;
        }

        public ISet<ComponentType> GetComponents()
        {
            ISet<ComponentType> innerSet = _inner.GetComponents();
            innerSet.Add(ComponentType.REPRODUCER);
            return innerSet;
        }
    }
}
