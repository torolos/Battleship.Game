using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lib
{
    public abstract class Ship
    {
        public List<Coordinate> Coordinates { get; protected set; } = new List<Coordinate>();

        /// <summary>
        /// Event for that raises when ship is sank
        /// </summary>
        public event EventHandler ShipSank;
        /// <summary>
        /// The hits the ship has received
        /// </summary>
        public int Hits { get; private set; }
        /// <summary>
        /// The ship's name
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The ship's size
        /// </summary>
        public abstract byte Size { get; }
        /// <summary>
        /// Method that attempts a hit to a ship
        /// </summary>
        /// <param name="coordinate">The coordinate the player selected</param>
        public bool Hit(Coordinate coordinate)
        {
            if (ValidCoordinate(coordinate))
            {
                Coordinates.Remove(coordinate);
                Hits += 1;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Initialises the ship 'piece'
        /// </summary>
        public void Init()
        {
            Hits = 0;
            this.Coordinates.Clear();
        }
        /// <summary>
        /// Sets the position of a piece on the board
        /// </summary>
        /// <param name="coordinates">The set of coordinates the ship will lie on</param>
        public bool SetPosition(IList<Coordinate> coordinates)
        {
            var fits = CanFit(coordinates);
            if (fits)
            {
                this.Coordinates.AddRange(coordinates);
            }
            return fits;
        }
        /// <summary>
        /// Checks whether ship lies on the coordinate
        /// </summary>
        /// <param name="coordinate">The string value of the coordinate</param>
        /// <returns></returns>
        public bool ValidCoordinate(Coordinate coordinate)
        {
            return Coordinates.Any(c => c.Equals(coordinate));
        }
        /// <summary>
        /// Checks whether all coordinates of the ship have been used
        /// </summary>
        /// <returns></returns>
        public bool IsShipSank()
        {
            return Hits == Size;
        }

        private bool CanFit(IList<Coordinate> coordinates)
        {
            if (!this.Coordinates.Any())
            {
                return true;
            }
            return this.Coordinates.Any(c => coordinates.Contains(c));
        }

        private void Sink()
        {
            Volatile.Read(ref ShipSank)?.Invoke(this, new EventArgs());
        }
    }
}
