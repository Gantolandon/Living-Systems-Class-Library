using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Living_Systems_Class_Library;
using Living_Systems_Class_Library.Helpers;
using Living_Systems_Class_Library.Components;

namespace Living_Systems_Class_Library_Test
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void TestReproducerProcessCreation()
        {
            LivingSystem system = new LivingSystem();
            MatterEnergyPile input = new MatterEnergyPile();
            MatterEnergyPile output = new MatterEnergyPile();
            input.AddAmount("matterEnergy", 1.5d);
            BasicProcessTemplate template = new BasicProcessTemplate();
            template.inputs = new Dictionary<string, double>();
            template.inputs.Add("matterEnergy", 1.0d);
            template.outputs = new Dictionary<string, double>();
            template.outputs.Add("matterEnergy", 0.5d);
            ProcessReproducerExecuteArgs executeArgs = new ProcessReproducerExecuteArgs();
            executeArgs.inputPile = input;
            executeArgs.outputPile = output;
            system.AddProcess("reproducerProcess", ProcessType.REPRODUCER, template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(system.ExecuteAllProcesses());
            Assert.IsNotNull(executeArgs.system);
            Assert.IsTrue(input["matterEnergy"] == 0.5d);
            Assert.IsTrue(output["matterEnergy"] == 0.5d);
        }

        [TestMethod]
        public void TestReproducerProcessFailure()
        {
            LivingSystem system = new LivingSystem();
            MatterEnergyPile input = new MatterEnergyPile();
            MatterEnergyPile output = new MatterEnergyPile();
            input.AddAmount("matterEnergy", 0.5d);
            BasicProcessTemplate template = new BasicProcessTemplate();
            template.inputs = new Dictionary<string, double>();
            template.inputs.Add("matterEnergy", 1.0d);
            template.outputs = new Dictionary<string, double>();
            template.outputs.Add("matterEnergy", 0.5d);
            ProcessReproducerExecuteArgs executeArgs = new ProcessReproducerExecuteArgs();
            executeArgs.inputPile = input;
            executeArgs.outputPile = output;
            system.AddProcess("reproducerProcess", ProcessType.REPRODUCER, template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsFalse(system.ExecuteAllProcesses());
            Assert.IsNull(executeArgs.system);
            Assert.IsTrue(input["matterEnergy"] == 0.5d);
            Assert.ThrowsException<KeyNotFoundException>(() => output["matterEnergy"]);
        }
    }
}
