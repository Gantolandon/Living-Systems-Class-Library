﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public class LivingSystem
    {
        private Dictionary<string, IProcess> processes;

        public LivingSystem()
        {
            this.processes = [];
        }

        public void AddProcess(string name, IProcessTemplate template)
        {
            IProcessBuilder builder = ProcessBuilder.GetProcessBuilder();
            IProcess process = builder.Build(this, template);
            this.processes.Add(name, process);
        }

        public bool SetExecuteArgs(string name, IProcessExecuteArgs args)
        {
            if (!processes.ContainsKey(name))
            {
                return false;
            }
            this.processes[name].ExecuteArgs = args;
            args.ComponentTypes = this.processes[name].GetComponents();
            return true;
        }

        public bool ExecuteAllProcesses()
        {
            bool result = true;
            foreach (KeyValuePair<string, IProcess> processPair in this.processes)
            {
                result = result && processPair.Value.Execute();
            }
            return result;
        }
    }
}
