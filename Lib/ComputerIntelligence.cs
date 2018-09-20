using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ComputerIntelligence : IComputerIntelligence
    {
        public Coordinate NextCoordinate(IList<Coordinate> previousHits, IList<Coordinate> exclusions)
        {
            Coordinate coordinate;
            if (previousHits.Any())
            {
                
            }
            return GameUtility.CreateRandomCoordinate(exclusions);
        }
    }
}
