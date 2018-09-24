using System;
using System.Linq;
using Lib;
using NUnit.Framework;
using Moq;

namespace UnitTests
{
    [TestFixture]
    public class ShipsFactoryTests
    {
        [Test]
        public void CreateShipsTest_ShouldNotHaveCoordinatesUsedByTwoShips()
        {
            var settingsMock = new Mock<IGameSettings>();
            settingsMock.SetupGet(x => x.BoardSize).Returns(10);
            var factory = new ShipsFactory(settingsMock.Object);
            var counter = 0;
            while (counter < 50)
            {
                var ships = factory.CreateShips();
                var coordinatesAssigned = ships.SelectMany(c => c.Coordinates);
                var distinct = coordinatesAssigned.Distinct();

                Assert.AreEqual(coordinatesAssigned.Count(), distinct.Count());
                counter += 1;
            }
        }
    }
}
