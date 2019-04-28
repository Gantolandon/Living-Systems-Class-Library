using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library
{
    public enum ProcessType
    {
        REPRODUCER,
    }

    public interface IProcessBuilder
    {

        IProcess Build(LivingSystem system, ProcessType type, IProcessTemplate template);
    }
}
