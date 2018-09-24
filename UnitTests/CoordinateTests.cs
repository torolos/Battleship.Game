using System;
using NUnit.Framework;
using Lib;

namespace UnitTests
{
    [TestFixture]
    public class CoordinateTests
    {
        [TestCase("B5", 2, 5)]
        [TestCase("I9", 9, 9)]
        [TestCase("C10", 3, 10)]
        [TestCase("F3", 6, 3)]
        [TestCase("G8", 7, 8)]
        [TestCase("A1", 1, 1)]
        [TestCase("J10", 10, 10)]
        [TestCase("D6", 4, 6)]
        public void CoordinateResolveTest_1(string point, byte row, byte column)
        {
            var coordinate = new Coordinate(point);

            Assert.AreEqual(row, coordinate.Row);
            Assert.AreEqual(column, coordinate.Column);
        }
        [TestCase(2, 5, "B5")]
        [TestCase(9, 9, "I9")]
        [TestCase(3, 10, "C10")]
        [TestCase(6, 3, "F3")]
        [TestCase(7, 8, "G8")]
        [TestCase(1, 1, "A1")]
        [TestCase(10, 10, "J10")]
        [TestCase(4, 6, "D6")]
        public void CoordinateResolveTest_2(byte row, byte column, string expected)
        {
            var coordinate = new Coordinate(row, column);

            Assert.AreEqual(expected, coordinate.DisplayName);
        }
        [TestCase(2, 5, 2, 5, true)]
        [TestCase(9, 9, 8, 2, false)]
        [TestCase(5, 3, 1, 10, false)]
        [TestCase(8, 2, 8, 2, true)]
        [TestCase(1, 1, 1, 2, false)]
        [TestCase(5, 5, 5, 5, true)]
        [TestCase(10, 1, 1, 10, false)]
        [TestCase(8, 9, 1, 9, false)]
        [TestCase(1, 2, 1, 3, false)]
        [TestCase(3, 9, 3, 9, true)]
        public void CoordinateEqualityTest(byte row1, byte column1, byte row2, byte column2, bool expected)
        {
            var coordinate1 = new Coordinate(row1, column1);
            var coordinate2 = new Coordinate(row2, column2);

            Assert.AreEqual(expected, coordinate1.Equals(coordinate2));
        }

        [TestCase(12, 1)]
        [TestCase(1, 12)]
        [TestCase(23, 12)]
        [TestCase(0, 9)]
        [TestCase(9, 0)]
        [TestCase(12, 0)]
        [TestCase(0, 12)]
        public void TryCreateInvalidCoordinates1(byte row, byte column)
        {
            Assert.Throws<ArgumentException>(() => new Coordinate(row, column));
        }

        [TestCase("999")]
        [TestCase("M1")]
        [TestCase("A11")]
        [TestCase("M32")]
        [TestCase("BB")]
        [TestCase("CCCC")]
        [TestCase("9999991111")]
        [TestCase("dslafsfi")]
        public void TryCreateInvalidCoordinates2(string point)
        {
            Assert.Throws<ArgumentException>(() => new Coordinate(point));
        }
        [TestCase("a8")]
        [TestCase("f1")]
        [TestCase("d7")]
        [TestCase("h10")]
        [TestCase("b4")]
        [TestCase("g5")]
        public void TryCreateCoordinatesWithLowerCase(string point)
        {
            var coordinate = new Coordinate(point);
            Assert.AreEqual(coordinate.DisplayName, point.ToUpper());
        }
    }
}
