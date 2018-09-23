using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Lib;
using Moq;

namespace UnitTests
{ 
    [TestFixture]
    public class CoordinateHelperTests
    {
        [Theory()]
        [TestCase("A2", new string[] { "A1", "A3", "B2" })]
        [TestCase("J10", new string[] { "J9", "I10" })]
        [TestCase("D5", new string[] { "D4", "C5", "D6", "E5" })]
        [TestCase("F1", new string[] { "E1", "F2", "G1" })]
        [TestCase("A1", new string[] { "A2", "B1" })]
        [TestCase("G8", new string[] { "G7", "F8", "G9", "H8" })]
        public void GetPotentialCandidatesBasedOnOneCoordinate(string point, string[] results)
        {
            var settingsMock = new Mock<IGameSettings>();
            settingsMock.SetupGet(c => c.BoardSize).Returns(10);
            var coordinate = new Coordinate(point);
            var expected = results.Select(c => new Coordinate(c));
            var helper = new CoordinateHelper(settingsMock.Object);
            var adjacent = helper.GetAdjacent(coordinate);

            var matching = adjacent.Count(c => expected.Contains(c)) == expected.Count();

            Assert.AreEqual(expected.Count(), adjacent.Count());
            Assert.True(matching);
        }
        [Theory()]
        [TestCase(new string[] { "A2", "A3" }, new string[] { "A1", "A4" })]
        [TestCase(new string[] { "F4", "F6" }, new string[] { "F3", "F5", "F7" })]
        [TestCase(new string[] { "B1", "B2" }, new string[] { "B3" })]
        [TestCase(new string[] { "B1", "B2", "B3" }, new string[] { "B4" })]
        [TestCase(new string[] { "G3", "G4", "G5", "G6" }, new string[] { "G2", "G7" })]
        [TestCase(new string[] { "A1", "A3" }, new string[] { "A2", "A4" })]
        [TestCase(new string[] { "B4", "B5", "B7" }, new string[] { "B6", "B8", "B3" })]
        public void GetPotentialCandidatesBasedOnMultipleCoordinate(string[] points, string[] results)
        {
            var settingsMock = new Mock<IGameSettings>();
            settingsMock.SetupGet(c => c.BoardSize).Returns(10);
            var expected = results.Select(c => new Coordinate(c)).ToArray();
            var adjacent = new CoordinateHelper(settingsMock.Object).GetAdjacent(points.Select(c => new Coordinate(c)).ToArray());

            var matching = adjacent.Count(c => expected.Contains(c)) == expected.Count();

            Assert.AreEqual(expected.Count(), adjacent.Count());
            Assert.True(matching);
        }
    }
}
