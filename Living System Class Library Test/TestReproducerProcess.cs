using Living_Systems_Class_Library;
using Living_Systems_Class_Library.Helpers;

namespace Living_Systems_Class_Library_Test
{
    [TestClass]
    public class TestReproducerProcess
    {
        [TestMethod]
        public void TestReproducerProcessCreation()
        {
            LivingSystem system = new LivingSystem();
            MatterEnergyPile input = new MatterEnergyPile();
            MatterEnergyPile output = new MatterEnergyPile();
            input.AddAmount("matterEnergy", 1.5d);
            dynamic template = new BasicProcessTemplate();
            template.Inputs = new Dictionary<string, double>();
            template.Inputs.Add("matterEnergy", 1.0d);
            template.Outputs = new Dictionary<string, double>();
            template.Outputs.Add("matterEnergy", 0.5d);
            ISet<ComponentType> types = new SortedSet<ComponentType>();
            types.Add(ComponentType.REPRODUCER);
            template.ComponentTypes = types;
            dynamic executeArgs = new BasicProcessExecuteArgs();
            executeArgs.InputPile = input;
            executeArgs.OutputPile = output;
            system.AddProcess("reproducerProcess", template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(system.ExecuteAllProcesses());
            Assert.IsNotNull(executeArgs.System);
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
            dynamic template = new BasicProcessTemplate();
            template.Inputs = new Dictionary<string, double>();
            template.Inputs.Add("matterEnergy", 1.0d);
            template.Outputs = new Dictionary<string, double>();
            template.Outputs.Add("matterEnergy", 0.5d);
            ISet<ComponentType> types = new SortedSet<ComponentType>();
            types.Add(ComponentType.REPRODUCER);
            template.ComponentTypes = types;
            dynamic executeArgs = new BasicProcessExecuteArgs();
            executeArgs.InputPile = input;
            executeArgs.OutputPile = output;
            system.AddProcess("reproducerProcess", template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsFalse(system.ExecuteAllProcesses());
            Assert.IsNull(executeArgs.System);
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
            dynamic template = new BasicProcessTemplate();
            ISet<ComponentType> types = new SortedSet<ComponentType>();
            types.Add(ComponentType.REPRODUCER);
            template.ComponentTypes = types;
            template.Inputs = new Dictionary<string, double>();
            template.Inputs.Add("matterEnergy", 1.0d);
            template.Outputs = new Dictionary<string, double>();
            template.Outputs.Add("matterEnergy", 0.5d);
            template.ProcessesToAdd = new Dictionary<string, IProcessTemplate>();
            template.ProcessesToAdd.Add("reproducerProcess", template);
            dynamic executeArgs = new BasicProcessExecuteArgs();
            executeArgs.InputPile = input;
            executeArgs.OutputPile = output;
            system.AddProcess("reproducerProcess", template);
            Assert.IsTrue(system.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(system.ExecuteAllProcesses());
            LivingSystem secondSystem = executeArgs.System;
            Assert.IsNotNull(secondSystem);
            executeArgs.System = null;
            Assert.IsTrue(secondSystem.SetExecuteArgs("reproducerProcess", executeArgs));
            Assert.IsTrue(secondSystem.ExecuteAllProcesses());
            LivingSystem thirdSystem = executeArgs.System;
            Assert.IsNotNull(thirdSystem);
        }
    }
}
