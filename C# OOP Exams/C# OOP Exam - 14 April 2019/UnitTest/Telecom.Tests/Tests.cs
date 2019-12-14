namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        private Phone phone;

        [SetUp]
        public void Setup()
        {
            this.phone = new Phone("Google","Pixel");
        }

        [Test]
        public void CounterShouldReturnCorrectResult()
        {
            Assert.AreEqual(0, phone.Count);
        }

        [Test]
        public void MakeShouldNotBeNull()
        {
            Assert.Throws<ArgumentException>(()=> new Phone(null,"Pixel"));
        }

        [Test]
        public void MakeShouldNotBeEhiteSpace()
        {
            Assert.Throws<ArgumentException>(() => new Phone(string.Empty, "Pixel"));
        }

        [Test]
        public void MakeShouldBeCorect()
        {
            Assert.AreEqual("Google",this.phone.Make);
        }

        [Test]
        public void ModelShouldNotBeNull()
        {
            Assert.Throws<ArgumentException>(() => new Phone("Google", null));
        }

        [Test]
        public void ModelShouldNotBeWhiteSpace()
        {
            Assert.Throws<ArgumentException>(() => new Phone("Google", string.Empty));
        }

        [Test]
        public void ModelShouldBeCorrect()
        {
            Assert.AreEqual("Pixel",this.phone.Model);
        }

        [Test]
        public void AddConctactShouldThrowInvalidOperationExceptionWhenContactExists()
        {
            this.phone.AddContact("Ivan", "0123456789");
            Assert.Throws<InvalidOperationException>(() => this.phone.AddContact("Ivan","123456789"));
        }

        [Test]
        public void AddConctactCorrectrly()
        {
            this.phone.AddContact("Ivan", "0123456789");
            Assert.AreEqual(1, this.phone.Count);
        }

        [Test]
        public void CallConctactShouldThrowInvalidOperationExceptionWhenContactNotExist()
        {           
            Assert.Throws<InvalidOperationException>(() => this.phone.Call("Vesko"));
        }

        [Test]
        public void CallConctactCorrectly()
        {
            this.phone.AddContact("Ivan", "0123456789");
            var result = this.phone.Call("Ivan");
            var wanted = $"Calling Ivan - 0123456789...";

            Assert.AreEqual(result, wanted);
        }

    }
}