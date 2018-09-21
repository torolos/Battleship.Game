using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class CoordinateHelper
    {
        public delegate bool Move(out Coordinate coordinate);
        public Coordinate[] GetAdjacentNeighbours(Coordinate coordinate, int boardSize)
        {
            var funcs = new List<Move>();
            if (coordinate.Row > 1)
                funcs.Add(coordinate.Up);
            if (coordinate.Row < boardSize)
                funcs.Add(coordinate.Down);
            if (coordinate.Column > 1)
                funcs.Add(coordinate.Left);
            if (coordinate.Column < boardSize)
                funcs.Add(coordinate.Right);

            var result = new List<Coordinate>();
            foreach (var func in funcs)
            {
                if (func(out Coordinate coor))
                {
                    result.Add(coor);
                }
            }
            return result.ToArray();
        }

        public Coordinate[] GetAdjacentNeighbours(Coordinate[] coordinates, int boardSize)
        {
            if (coordinates.Count() == 1)
                return GetAdjacentNeighbours(coordinates.First(), boardSize);
            Func<Coordinate, bool> check = null;
            if (coordinates.Select(c => c.Row).Distinct().Count() == 1)
            {
                check = cr => cr.Row == coordinates.First().Row;
            }
            if (coordinates.Select(c => c.Column).Distinct().Count() == 1)
            {
                check = cr => cr.Column == coordinates.First().Column;
            }
            var available = coordinates.Select(c => GetAdjacentNeighbours(c, boardSize)).SelectMany(c => c).ToArray();

            return available.Where(c => check(c) && !coordinates.Contains(c)).Distinct().ToArray();
        }


    }
}
