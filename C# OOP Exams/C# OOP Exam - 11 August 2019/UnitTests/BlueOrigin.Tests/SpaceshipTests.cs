namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    public class SpaceshipTests
    {
        private Astronaut astronaut1;
        private Astronaut astronaut2;
        private Astronaut astronaut3;

        private Spaceship spaceship;

        [SetUp]
        public void SetUp()
        {
            this.astronaut1 = new Astronaut("Name1", 10);
            this.astronaut2 = new Astronaut("Name2", 20);
            this.astronaut3 = new Astronaut("Name3", 30);

            this.spaceship = new Spaceship("Space", 3);
        }

        [Test]
        public void Test_Constructor()
        {
            Assert.AreEqual("Name1", this.astronaut1.Name);
            Assert.AreEqual(10, this.astronaut1.OxygenInPercentage);

            Assert.AreEqual("Space", this.spaceship.Name);
            Assert.AreEqual(3, this.spaceship.Capacity);
            Assert.AreEqual(0, this.spaceship.Count);
        }

        [Test]
        public void Test_Counter()
        {
            this.spaceship.Add(this.astronaut1);
            this.spaceship.Add(this.astronaut2);

            Assert.AreEqual(2, this.spaceship.Count);
        }

        [Test]
        public void Name_Can_Not_Be_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Spaceship(null, 10));
        }

        [Test]
        public void Capacity_Can_Not_Be_Negative()
        {
            Assert.Throws<ArgumentException>(() => new Spaceship("Name2", -1));
        }

        [Test]
        public void Can_Not_Add_More_Than_Capacity()
        {
            this.spaceship.Add(this.astronaut1);
            this.spaceship.Add(this.astronaut2);
            this.spaceship.Add(this.astronaut3);

            Assert.Throws<InvalidOperationException>(() => this.spaceship.Add(new Astronaut("Name4",2)));
        }

        [Test]
        public void Can_Not_Add_Same_Name()
        {
            this.spaceship.Add(this.astronaut1);
            
            Assert.Throws<InvalidOperationException>(() => this.spaceship.Add(this.astronaut1));
        }

        [Test]
        public void Add_Name_Normally()
        {
            this.spaceship.Add(this.astronaut1);

            Assert.AreEqual(1, this.spaceship.Count);
        }

        [Test]
        public void Remove_Normally()
        {
            this.spaceship.Add(this.astronaut1);
            Assert.AreEqual(true, this.spaceship.Remove(this.astronaut1.Name));
        }

        [Test]
        public void Remove_UnNormally()
        {
            this.spaceship.Add(this.astronaut1);
            Assert.AreEqual(false, this.spaceship.Remove(this.astronaut2.Name));
        }
    }
}