using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <inheritDoc />
    public class CoordinateHelper: ICoordinateUtility
    {
        private delegate bool Move(out Coordinate coordinate);
        private readonly IGameSettings settings;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">The <see cref="IGameSettings"/> instance</param>
        public CoordinateHelper(IGameSettings settings)
        {
            this.settings = settings;
        }
        /// <inheritDoc />
        public IList<Coordinate> GetAdjacent(Coordinate coordinate)
        {
            var funcs = new List<Move>();
            // If specified coordinate does not contain top row
            if (coordinate.Row > 1)
                funcs.Add(coordinate.Up);
            // If specified coordinate does not contain bottom row
            if (coordinate.Row < settings.BoardSize)
                funcs.Add(coordinate.Down);
            // If specified coordinate does not contain outer left column
            if (coordinate.Column > 1)
                funcs.Add(coordinate.Left);
            // If specified coordinate does not containt outer right column
            if (coordinate.Column < settings.BoardSize)
                funcs.Add(coordinate.Right);

            var result = new List<Coordinate>();
#pragma warning disable
            funcs.ForEach(f =>
            {
                if (f(out Coordinate c))
                    result.Add(c);
            });
#pragma warning restore
            
            return result.ToArray();
        }
        /// <inheritDoc />
        public IList<Coordinate> GetAdjacent(IList<Coordinate> coordinates)
        {
            // if single coordinate then get adjacencies for this one
            if (coordinates.Count() == 1)
                return GetAdjacent(coordinates.First());

            Func<Coordinate, bool> check = null;
            // Check if coordinates in same row
            if (coordinates.Select(c => c.Row).Distinct().Count() == 1)
            {
                check = cr => cr.Row == coordinates.First().Row;
            }
            // Check if coordinates in same column
            if (coordinates.Select(c => c.Column).Distinct().Count() == 1)
            {
                check = cr => cr.Column == coordinates.First().Column;
            }
            // Get total available for specified coordinates
            var available = coordinates.Select(c => GetAdjacent(c)).SelectMany(c => c).ToArray();
            // Filter out specificed coordinates and return distinct result
            return available.Where(c => check(c) && !coordinates.Contains(c)).Distinct().ToList();
        }
        /// <inheritDoc />
        public bool TryGetAdjacent(IList<Coordinate> coordinates, 
            IList<Coordinate> exclusions, out IList<Coordinate> result)
        {
            result = new List<Coordinate>();
            var res = GetAdjacent(coordinates);
            if (res.Any())
            {
                result = res.Where(c => !exclusions.Contains(c)).ToList();
                return result.Any();
            }
            return false;
        }
    }
}
