using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ComputerPlayer: Player
    {
        private List<Coordinate> successfulHits = new List<Coordinate>();
        /// <summary>
        /// Method that automatically plays a hit for the computer
        /// </summary>
        /// <param name="opponent">The opponent to strike</param>
        /// <returns>A <see cref="AttemptResult"/> result</returns>
        public AttemptResult AutoPlay(Player opponent)
        {
            var coordinate = GameUtility.CreateRandomCoordinate(usedCoordinates);
            // If computer had a successful hit attempt to strike neighbouring coordinates
            if (successfulHits.Any())
            {
                coordinate = GetCoordinateFromSuccessful();
            }
            return HitOpponent(opponent, coordinate);
        }

        private Coordinate GetCoordinateFromSuccessful()
        {
            if (successfulHits.Count == 1)
            {
                var candidates = new TileInfo(successfulHits.First(), GameUtility.BOARD_SIZE).AvailableCoordinates;
                return candidates.Random().Value.First();
            }
            else
            {

            }
            return GameUtility.CreateRandomCoordinate(usedCoordinates);
        }
    }
}
