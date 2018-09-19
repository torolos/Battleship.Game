using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Class that provides information for a selected coordinate
    /// </summary>
    public class TileInfo
    {
        private readonly byte boardSize;
        /// <summary>
        /// The current coordinate
        /// </summary>
        public Coordinate Coordinate { get; }
        /// <summary>
        /// The tiles available for selected coordinate
        /// </summary>
        public Dictionary<Direction, List<Coordinate>> AvailableCoordinates { get; } =
            new Dictionary<Direction, List<Coordinate>>();
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="boardSize">The board size</param>
        public TileInfo(Coordinate coordinate, byte boardSize)
        {
            this.boardSize = boardSize;
            Coordinate = coordinate;
            CalculateAdjacencies();
        }

        /// <summary>
        /// Calculates the tiles on each direction of the board. The method takes
        /// into account the current coordinate and looks for the remaining ones.
        /// For example for a destroyer with size of 4 this method will be performed
        /// for an additional 3 tiles
        /// </summary>
        /// <param name="shipSize">The additional number of coordinates required</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> instance of the available coordinates</returns>
        public IDictionary<Direction, List<Coordinate>> GetCandidateTiles(byte shipSize)
        {
            var result = new Dictionary<Direction, List<Coordinate>>();
            AvailableCoordinates.ForEach(c =>
            {
                if (c.Value.Count >= shipSize - 1)
                {
                    result.Add(c.Key, c.Value.Take(shipSize).ToList());
                }
            });
            return result;
        }
        /// <summary>
        /// Return direct neighbours of current coordinate
        /// </summary>
        /// <returns>A list of coordinates</returns>
        public IList<Coordinate> GetNeighbours()
        {
            return AvailableCoordinates.Select(c => c.Value.First()).ToList();
        }

        /// <summary>
        /// Performs equality based on coordinate
        /// </summary>
        /// <param name="obj">The <see cref="TileInfo"/> to compare</param>
        /// <returns>returns true if objects equal</returns>
        public override bool Equals(object obj)
        {
            var tile = (TileInfo)obj;
            return tile.Coordinate.Equals(this.Coordinate);
        }
        /// <summary>
        /// Return hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region private
        private void CalculateAdjacencies()
        {
            AvailableCoordinates.Add(Direction.Up, new List<Coordinate>());
            AvailableCoordinates.Add(Direction.Down, new List<Coordinate>());
            AvailableCoordinates.Add(Direction.Left, new List<Coordinate>());
            AvailableCoordinates.Add(Direction.Right, new List<Coordinate>());
            var counter = 0;
            byte toByte(int value)
            {
                return Convert.ToByte(value);
            }

            counter = Coordinate.Row - 1;
            while (counter > 0)
            {
                AvailableCoordinates.First(c => c.Key == Direction.Up).Value.Add(new Coordinate(toByte(counter), Coordinate.Column));
                counter -= 1;
            }
            counter = Coordinate.Row + 1;
            while (counter <= boardSize)
            {
                AvailableCoordinates.First(c => c.Key == Direction.Down).Value.Add(new Coordinate(toByte(counter), Coordinate.Column));
                counter += 1;
            }
            counter = Coordinate.Column - 1;
            while (counter > 0)
            {
                AvailableCoordinates.First(c => c.Key == Direction.Left).Value.Add(new Coordinate(Coordinate.Row, toByte(counter)));
                counter -= 1;
            }
            counter = Coordinate.Column + 1;
            while (counter <= boardSize)
            {
                AvailableCoordinates.First(c => c.Key == Direction.Right).Value.Add(new Coordinate(Coordinate.Row, toByte(counter)));
                counter += 1;
            }            
        }
        #endregion
    }
}
