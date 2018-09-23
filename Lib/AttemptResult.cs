using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Class containing information about a result of a strike
    /// </summary>
    public class AttemptResult
    {
        /// <summary>
        /// The <see cref="ResultType"/> value
        /// </summary>
        public ResultType ResultType { get; }
        /// <summary>
        /// The name of the ship recieving a hit (will be null on a miss)
        /// </summary>
        public string ShipName { get; }
        /// <summary>
        /// The coordinate that was used
        /// </summary>
        public Coordinate Coordinate { get; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="resultType">The <see cref="ResultType"/></param>
        /// <param name="shipName">The ship name</param>
        /// <param name="coordinate">The coordinate</param>
        public AttemptResult(ResultType resultType, 
            string shipName, Coordinate coordinate)
        {
            ResultType = resultType;
            ShipName = shipName;
            Coordinate = coordinate;
        }

        public override string ToString()
        {
            switch(ResultType)
            {
                case ResultType.Miss:
                case ResultType.Hit:
                    return $"Attempt at {Coordinate.DisplayName} -> {ResultType.ToString()}";
                case ResultType.Sink:                    
                    return $"Hit at {Coordinate.DisplayName} sinks {ShipName}";
                default:
                    return "Unkown";
            }
        }
    }
}
