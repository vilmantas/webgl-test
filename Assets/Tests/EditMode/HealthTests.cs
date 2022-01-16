using Modules;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class HealthTests
    {
        private Health _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Health();
        }

        [Test]
        public void Health_HasValueTest()
        {
            Assert.AreEqual(Health.DEFAULT, _sut.Value);
        }

        [Test]
        public void Health_CanLoseHealth()
        {
            _sut.Lose(1f);
            Assert.AreEqual(4, _sut.Value);
        }

        [Test]
        public void Health_CanGainHealth()
        {
            _sut.Gain(1f);
            Assert.AreEqual(6, _sut.Value);
        }

        [Test]
        public void Health_CanSetStartingHealth()
        {
            _sut = new Health(10);
            Assert.AreEqual(10, _sut.Value);
        }

        [Test]
        public void Health_HasMaxValue()
        {
            Assert.AreEqual(Health.DEFAULT, _sut.Max);
        }
    }
}