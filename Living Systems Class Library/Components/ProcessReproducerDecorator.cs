using Living_Systems_Class_Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Systems_Class_Library.Components
{
    class ProcessReproducerDecorator: IProcess
    {
        private IProcess _inner;

        public ProcessReproducerDecorator(IProcess inner)
        {
            this._inner = inner;
        }

        public bool Execute()
        {
            return _inner.Execute();
        }

        public bool Execute(MatterEnergyPile input, MatterEnergyPile output)
        {
            bool result = _inner.Execute(input, output);
            if (result)
            {
                result = result && Execute();
            }
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
