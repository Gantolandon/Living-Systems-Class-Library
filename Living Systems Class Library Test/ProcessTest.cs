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
            template.type = ProcessType.REPRODUCER;
            ReproducerProcessExecuteArgs executeArgs = new ReproducerProcessExecuteArgs();
            executeArgs.inputPile = input;
            executeArgs.outputPile = output;
            system.AddProcess("reproducerProcess", template);
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
            template.type = ProcessType.REPRODUCER;
            ReproducerProcessExecuteArgs executeArgs = new ReproducerProcessExecuteArgs();
            executeArgs.inputPile = input;
            executeArgs.outputPile = output;
            system.AddProcess("reproducerProcess", template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsFalse(system.ExecuteAllProcesses());
            Assert.IsNull(executeArgs.system);
            Assert.IsTrue(input["matterEnergy"] == 0.5d);
            Assert.ThrowsException<KeyNotFoundException>(() => output["matterEnergy"]);
        }
        [TestMethod]
        public void TestReproducerProcessMultiple()
        {
            LivingSystem system = new LivingSystem();
            MatterEnergyPile input = new MatterEnergyPile();
            MatterEnergyPile output = new MatterEnergyPile();
            input.AddAmount("matterEnergy", 2.0d);
            ReproducerProcessTemplate template = new ReproducerProcessTemplate();
            template.inputs = new Dictionary<string, double>();
            template.inputs.Add("matterEnergy", 1.0d);
            template.outputs = new Dictionary<string, double>();
            template.outputs.Add("matterEnergy", 0.5d);
            template.type = ProcessType.REPRODUCER;
            template.processesToAdd = new Dictionary<string, IProcessTemplate>();
            template.processesToAdd.Add("reproducerProcess", template);
            ReproducerProcessExecuteArgs executeArgs = new ReproducerProcessExecuteArgs();
            executeArgs.inputPile = input;
            executeArgs.outputPile = output;
            system.AddProcess("reproducerProcess", template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(system.ExecuteAllProcesses());
            LivingSystem secondSystem = executeArgs.system;
            Assert.IsNotNull(secondSystem);
            executeArgs.system = null;
            Assert.IsTrue(secondSystem.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(secondSystem.ExecuteAllProcesses());
            LivingSystem thirdSystem = executeArgs.system;
            Assert.IsNotNull(thirdSystem);
        }
    }
}
