using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ComputerPlayer: Player, IComputerPlayer
    {
        private List<Coordinate> successfulHits = new List<Coordinate>();
        private readonly ICoordinateUtility coordinateUtility;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="computerUtility">A <see cref="ICoordinateUtility"/> object</param>
        public ComputerPlayer(ICoordinateUtility coordinateUtility, IGameSettings gameSettings): base("Computer", gameSettings)
        {
            this.coordinateUtility = coordinateUtility;
        }
        /// <inheritDoc />
        public AttemptResult AutoPlay(IPlayer opponent)
        {
            
            var coordinate = GameUtility.CreateRandomCoordinate(Used());
            // If computer had a successful hit attempt to strike neighbouring coordinates
            if (successfulHits.Any())
            {
                coordinate = GetCoordinateFromSuccessful();
            }
            return HitOpponent(opponent, coordinate);
        }

        private Coordinate GetCoordinateFromSuccessful()
        {
            if (coordinateUtility.TryGetAdjacent(successfulHits, Used(), out IList<Coordinate> result))
            {
                return result[GameUtility.CreateRandom(0, result.Count())];
            }
            return GameUtility.CreateRandomCoordinate(Used());
        }
    }
}
