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
            var coordinate = GameUtility.CreateRandomCoordinate(Used(), gameSettings.BoardSize);
            // If computer had a successful hit attempt to strike neighbouring coordinates
            if (successfulHits.Any())
            {
                coordinate = GetCoordinateFromSuccessful();
            }
            var result = HitOpponent(opponent, coordinate);
            HandleResult(result.ResultType, coordinate);
            return result;
        }

        private Coordinate GetCoordinateFromSuccessful()
        {
            if (coordinateUtility.TryGetAdjacent(successfulHits, Used(), out IList<Coordinate> result))
            {
                return result[GameUtility.CreateRandom(0, result.Count())];
            }
            return GameUtility.CreateRandomCoordinate(Used(), gameSettings.BoardSize);
        }

        private void HandleResult(ResultType resultType, Coordinate coor)
        {
            if (resultType == ResultType.Hit)
                successfulHits.Add(coor); // Add a success into the buffer to be used for next hit
            if (resultType == ResultType.Sink || resultType == ResultType.GameEnds)
                successfulHits.Clear(); // Clear buffer after sinking a ship
        }
    }
}
