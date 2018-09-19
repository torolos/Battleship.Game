using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        private ShipList shipList;
        protected List<Coordinate> usedCoordinates = new List<Coordinate>();
        /// <summary>
        /// Ctor
        /// </summary>
        public Player()
        {
            Init(); 
        }
        /// <summary>
        /// Initialises player
        /// </summary>
        public void Init()
        {
            shipList = new ShipList(new ShipsFactory());
            usedCoordinates.Clear();
        }
        /// <summary>
        /// Resets player
        /// </summary>
        public void Reset()
        {
            Init();
        }
        /// <summary>
        /// Method for checking received hit
        /// </summary>
        /// <param name="coordinate">The coordinate to check</param>
        /// <returns>A <see cref="AttemptResult"/></returns>
        public AttemptResult Strike(Coordinate coordinate)
        {
            return shipList.TryReceivesStrike(coordinate);
        }
        /// <summary>
        /// Attempts a hit to the specified <see cref="Player"/> opponent.
        /// </summary>
        /// <param name="opponent">The opponent</param>
        /// <param name="coordinate">The coordinate to try</param>
        /// <returns>A <see cref="AttemptResult"/> instance</returns>
        public AttemptResult HitOpponent(Player opponent, Coordinate coordinate)
        {
            if (opponent.Equals(this))
            {
                // TODO: Handle self hit
            }
            if (usedCoordinates.Contains(coordinate))
            {
                return new AttemptResult(ResultType.Used, null, coordinate);
            }
            usedCoordinates.Add(coordinate);
            return opponent.Strike(coordinate);
        }
    }
}
