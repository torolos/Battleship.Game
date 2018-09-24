using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// inheritDoc
    public class ShipsFactory
    {
        private readonly IGameSettings gameSettings;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameSettings"></param>
        public ShipsFactory(IGameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
        }
        /// inheritDoc
        public IList<Ship> CreateShips()
        {
            var gameSize = gameSettings.BoardSize;
            var ships = GetShips();
            var unusableCoordinates = new List<Coordinate>();
            var result = new List<Ship>();

            #region getCandidates
            IDictionary<Direction, List<Coordinate>> getCandidates(ref Coordinate coordinate, byte size)
            {
                var tileInfo = new TileInfo(coordinate, gameSize);
                var candidates = tileInfo.GetCandidateTiles(size);
                while (!candidates.Any())
                {
                    unusableCoordinates.Add(coordinate);
                    coordinate = GameUtility.CreateRandomCoordinate(unusableCoordinates, gameSize);
                    tileInfo = new TileInfo(coordinate, gameSize);
                    candidates = tileInfo.GetCandidateTiles(size);
                }
                return candidates;
            }
            #endregion

            ships.ForEach(s =>
            {
                var shipPlaced = false;
                while (!shipPlaced) // While no coordinates have been set for the ship
                {
                    // Generate random coordinate
                    var coordinate = GameUtility.CreateRandomCoordinate(unusableCoordinates, gameSize);
                    // Get candidate coordinates. Returns the up,down,left,right coordinates for the specified ship size
                    var candidates = getCandidates(ref coordinate, s.Size);
                    var counter = 0;
                    // Loop to check that candidates aren't occupied by other ships
                    while (counter < candidates.Count)
                    { 
                        // Select random candidate list
                        var index = GameUtility.CreateRandom(0, candidates.Count);
                        var element = candidates.ElementAt(index);
                        // Check whether candidates are usable
                        var isUnusable = element.Value.Any(c => unusableCoordinates.Contains(c));
                        // Add the calculated coordinates to the unusable ones.
                        // Whether they will be used or not, they should not be used
                        // on the next cycle or next ship
                        unusableCoordinates.AddRange(element.Value);
                        if (isUnusable)
                        {                           
                            candidates.Remove(element.Key);
                            
                        }
                        else
                        {
                            // Set Ship position and flag ship as placed
                            var ordered = element.Value.Take(s.Size - 1);
                            var position = new List<Coordinate> { coordinate };
                            position.AddRange(ordered);
                            s.SetPosition(position);
                            shipPlaced = true;
                            break;
                        }
                        // Increment counter to try another set of candidates
                        counter += 1;
                    }
                }               
            });
            return ships;
        }

        private static List<Ship> GetShips()
        {
            return new List<Ship>
            {
                new Destroyer("Destroyer 1"),
                new Destroyer("Destroyer 2"),
                new Battleship()
            };
        }
    }
}
