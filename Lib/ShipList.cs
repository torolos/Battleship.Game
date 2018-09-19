using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ShipList
    {
        private readonly IList<Ship> ships;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shipsFactory">The <see cref="IShipsFactory"/> instance</param>
        public ShipList(IShipsFactory shipsFactory)
        {
            ships = shipsFactory.CreateShips();
        }
        /// <summary>
        /// Checks whether the received coordinate strikes a ship
        /// </summary>
        /// <param name="coordinate">The coordinate to check</param>
        /// <returns>A <see cref="AttemptResult"/> instance.</returns>
        public AttemptResult TryReceivesStrike(Coordinate coordinate)
        {
            var result = new AttemptResult(ResultType.Miss, null, coordinate);
            foreach(var ship in ships)
            {
                if (ship.Hit(coordinate))
                {
                    if (ship.IsShipSank())
                    {
                        ships.Remove(ship);
                        if (ships.Count == 0)
                        {
                            return new AttemptResult(ResultType.GameEnds, ship.Name, coordinate);
                        }
                        else
                        {
                            return new AttemptResult(ResultType.Sink, ship.Name, coordinate);
                        }                       
                    }
                    else
                    {
                        return new AttemptResult(ResultType.Hit, null, coordinate);
                    }
                }
            }
            return result;
        }

        
    }
}
