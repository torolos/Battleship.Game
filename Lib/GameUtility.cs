using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class GameUtility
    {
        private static Random random = new Random();

        public const int BOARD_SIZE = 10;

        /// <summary>
        /// Generates a random coordinate
        /// </summary>
        /// <returns>A <see cref="Coordinate"/> instance</returns>
        public static Coordinate CreateRandomCoordinate()
        {
            var max = BOARD_SIZE + 1;
            var row = Convert.ToByte(CreateRandom(1, max));
            var column = Convert.ToByte(CreateRandom(1, max));
            return new Coordinate(row, column);
        }
        /// <summary>
        /// Creates a random coordinate but excludes specified values
        /// </summary>
        /// <param name="excludeList">The list of coordinates the generator should exclude</param>
        /// <returns>A <see cref="Coordinate"/> instance</returns>
        public static Coordinate CreateRandomCoordinate(IList<Coordinate> excludeList)
        {
            var coordinate = CreateRandomCoordinate();
            while (excludeList.Contains(coordinate))
            {
                coordinate = CreateRandomCoordinate();
            }
            return coordinate;
        }
        /// <summary>
        /// Selects a random coordinate from a list
        /// </summary>
        /// <param name="coordinates">The list of coordinates to look into</param>
        /// <returns>The <see cref="Coordinate"/> instance</returns>
        public static Coordinate SelectRandom(IList<Coordinate> coordinates)
        {
            var index = CreateRandom(0, coordinates.Count);
            return coordinates[index];
        }
        /// <summary>
        /// Creates a random integer
        /// </summary>
        /// <param name="min">the minimum number</param>
        /// <param name="max">the maximum number. Contrary to OOTB <see cref="Random"/> this will increment
        /// max to include the upper bound in the generation.</param>
        /// <returns>An integer</returns>
        public static int CreateRandom(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
