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
    public class Player: IPlayer
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
        /// <inheritDoc />
        public void Init()
        {
            shipList = new ShipList();
            usedCoordinates.Clear();
        }
        /// <inheritDoc />
        public void Reset()
        {
            Init();
        }

        /// <inheritDoc />
        public AttemptResult Strike(Coordinate coordinate)
        {
            return shipList.TryReceivesStrike(coordinate);
        }
        /// <inheritDoc />
        public AttemptResult HitOpponent(IPlayer opponent, Coordinate coordinate)
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
