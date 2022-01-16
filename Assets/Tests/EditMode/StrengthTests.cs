using Modules;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    [TestFixture]
    public class StrengthTests
    {
        Strength _sut = new Strength();

        [SetUp]
        public void SetUp()
        {
            _sut = new Strength();
        }

        [Test]
        public void Strength_HasValueTest()
        {
            Assert.AreEqual(Strength.DEFAULT, _sut.Value);
        }
        
        [Test]
        public void Strength_CanLoseHealth()
        {
            _sut.Lose(1f);
            Assert.AreEqual(4, _sut.Value);
        }

        [Test]
        public void Strength_CanGainHealth()
        {
            _sut.Gain(1f);
            Assert.AreEqual(6, _sut.Value);
        }
        
        [Test]
        public void Health_CanSetStartingStrength()
        {
            _sut = new Strength(10);
            Assert.AreEqual(10, _sut.Value);
        }
    }
}