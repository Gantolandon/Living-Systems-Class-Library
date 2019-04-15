using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Living_Systems_Class_Library.Helpers;

namespace Living_Systems_Class_Library_Test
{
    [TestClass]
    public class MatterEnergyPileTest
    {
        [TestMethod]
        public void TestHasAmount()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            Assert.IsFalse(pile.HasAmount("matterEnergy", 1.0d));
            Assert.IsTrue(pile.HasAmount("matterEnergy", 0.0d));
        }

        [TestMethod]
        public void TestAddAmount()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            pile.AddAmount("matterEnergy", 1.0d);
            Assert.IsTrue(pile["matterEnergy"] == 1.0d);
            Assert.IsTrue(pile.HasAmount("matterEnergy", 1.0d));
            Assert.IsTrue(pile.HasAmount("matterEnergy", 0.5d));
            Assert.IsFalse(pile.HasAmount("matterEnergy", 1.5d));
        }

        [TestMethod]
        public void TestRemoveAmount()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            pile.AddAmount("matterEnergy", 1.0d);
            pile.RemoveAmount("matterEnergy", 0.5d);
            Assert.IsTrue(pile["matterEnergy"] == 0.5d);
            Assert.IsFalse(pile.HasAmount("matterEnergy", 1.0d));
            Assert.IsTrue(pile.HasAmount("matterEnergy", 0.5d));
            Assert.IsTrue(pile.HasAmount("matterEnergy", 0.25d));
        }

        [TestMethod]
        public void TestHasBulk()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            MatterEnergyPile pile2 = new MatterEnergyPile();
            MatterEnergyPile pile3 = new MatterEnergyPile();
            pile.AddAmount("matterEnergy1", 1.0d);
            pile2.AddAmount("matterEnergy1", 1.0d);
            pile.AddAmount("matterEnergy2", 1.0d);
            pile3.AddAmount("matterEnergy2", 1.0d);
            Assert.IsTrue(pile.HasBulk(pile));
            Assert.IsTrue(pile.HasBulk(pile2));
            Assert.IsTrue(pile.HasBulk(pile3));
            Assert.IsFalse(pile2.HasBulk(pile));
            Assert.IsFalse(pile3.HasBulk(pile));
            Assert.IsFalse(pile3.HasBulk(pile2));
        }

        [TestMethod]
        public void TestAddBulk()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            pile.AddAmount("matterEnergy1", 1.0d);
            MatterEnergyPile pile2 = new MatterEnergyPile();
            pile2.AddAmount("matterEnergy1", 1.0d);
            pile2.AddAmount("matterEnergy2", 1.0d);
            pile.AddBulk(pile2);
            Assert.AreEqual(pile["matterEnergy1"], 2.0d);
            Assert.AreEqual(pile["matterEnergy2"], 1.0d);
            Assert.IsTrue(pile.HasAmount("matterEnergy1", 2.0d));
            Assert.IsTrue(pile.HasAmount("matterEnergy2", 1.0d));
        }

        [TestMethod]
        public void TestRemoveBulk()
        {
            MatterEnergyPile pile = new MatterEnergyPile();
            pile.AddAmount("matterEnergy1", 2.0d);
            pile.AddAmount("matterEnergy2", 1.0d);
            MatterEnergyPile pile2 = new MatterEnergyPile();
            pile2.AddAmount("matterEnergy1", 1.0d);
            pile2.AddAmount("matterEnergy2", 1.0d);
            Assert.IsTrue(pile.RemoveBulk(pile2));
            Assert.AreEqual(pile["matterEnergy1"], 1.0d);
            Assert.ThrowsException<KeyNotFoundException>(() => pile["matterEnergy2"]);
            Assert.IsFalse(pile.HasAmount("matterEnergy1", 2.0d));
            Assert.IsFalse(pile.HasAmount("matterEnergy2", 1.0d));
        }
    }
}
