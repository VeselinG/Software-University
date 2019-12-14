using NUnit.Framework;
using System;

namespace TheRace.Tests
{
    [TestFixture]
    public class RaceEntryTests
    {
        private UnitRider unitRider1;
        private UnitRider unitRider2;
        private UnitMotorcycle unitMotorcycle1;
        private UnitMotorcycle unitMotorcycle2;
        private RaceEntry raceEntry;

        [SetUp]
        public void Setup()
        {
            this.unitMotorcycle1 = new UnitMotorcycle("Honda", 100, 50);
            this.unitMotorcycle2 = new UnitMotorcycle("Toyota", 60, 10);

            this.unitRider1 = new UnitRider("Vesko",unitMotorcycle1);
            this.unitRider2 = new UnitRider("Blaga",unitMotorcycle2);

            this.raceEntry = new RaceEntry();
        }

        [Test]
        public void CounterShouldReturnCorrectResult()
        {           
            Assert.AreEqual(0, raceEntry.Counter);
        }

        [Test]
        public void AddRiderShouldThrowInvalidOperationExceptionWhenRiderIsNull()
        {           
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddRider(null));
        }

        [Test]
        public void AddRiderShouldThrowInvalidOperationExceptionWhenRiderExists()
        {
            raceEntry.AddRider(this.unitRider1);

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddRider(this.unitRider1));
        }

        [Test]
        public void AddRiderShouldWorkCorrectly()
        {
            raceEntry.AddRider(this.unitRider1);

            Assert.AreEqual(1, raceEntry.Counter);
        }

        [Test]
        public void AddRiderShouldReturnMessageWhenAddIsSuccsesfull()
        {         
            var resultMessage = raceEntry.AddRider(unitRider1);

            var expectedMessage = $"Rider {unitRider1.Name} added in race.";

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [Test]
        public void CalculateAverageHorsePowerShouldThrowExceptionWhenRidersAreBellowMinParticipants()
        {          
            raceEntry.AddRider(this.unitRider1);

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePowerShouldReturnCorrectResult()
        {           
            raceEntry.AddRider(this.unitRider1);
            raceEntry.AddRider(this.unitRider2);

            var result = raceEntry.CalculateAverageHorsePower();
            var expectedResult = 80.00;

            Assert.AreEqual(expectedResult, result);
        }

    }
}