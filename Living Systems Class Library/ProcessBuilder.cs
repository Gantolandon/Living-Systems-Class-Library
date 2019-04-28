using Living_Systems_Class_Library.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public class ProcessBuilder : IProcessBuilder
    {
        private static ProcessBuilder processBuilderInstance = null;

        private ProcessBuilder()
        {

        }

        public static IProcessBuilder GetProcessBuilder()
        {
            if (processBuilderInstance == null)
            {
                processBuilderInstance = new ProcessBuilder();
            }
            return processBuilderInstance;
        }

        public IProcess Build(LivingSystem system, ProcessType type, IProcessTemplate template)
        {
            IProcess mainProcess = new Process(system);
            mainProcess.ProcessTemplate = template;
            switch (type)
            {
                case ProcessType.REPRODUCER:
                    IProcess reproducer = new ProcessReproducerDecorator(mainProcess);
                    return reproducer;
                default:
                    break;
            }
            return mainProcess;
        }
    }
}
