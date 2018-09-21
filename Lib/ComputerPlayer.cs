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
        private readonly IComputerIntelligence computerIntel;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="computerIntel">A <see cref="IComputerIntelligence"/> object</param>
        public ComputerPlayer(IComputerIntelligence computerIntel)
        {
            this.computerIntel = computerIntel;
        }
        /// <inheritDoc />
        public AttemptResult AutoPlay(IPlayer opponent)
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
