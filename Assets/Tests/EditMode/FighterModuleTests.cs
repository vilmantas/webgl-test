using System.Diagnostics.CodeAnalysis;
using Modules;
using NUnit.Framework;

namespace Tests.EditMode
{
    [TestFixture]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class FighterTests
    {
        private Fighter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Fighter();
        }

        [Test]
        public void Fighter_HasHealth()
        {
            Assert.AreEqual(Health.DEFAULT, _sut.Health);
        }
        
        [Test]
        public void Fighter_HasMaxHealth()
        {
            Assert.AreEqual(Health.DEFAULT, _sut.MaxHealth);
        }

        [Test]
        public void Fighter_HasPower()
        {
            Assert.AreEqual(Strength.DEFAULT, _sut.Power);
        }

        [Test]
        public void Fighter_LosesHealthWhenAttacked()
        {
            var attacker = new Fighter(1, 1, 1);
            attacker.Attack(_sut);
            Assert.AreEqual(_sut.MaxHealth - attacker.Power, _sut.Health);
        }
     }
}