using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Interface for factory classes creating the ship collections
    /// that will be available
    /// </summary>
    public interface IShipsFactory
    {
        /// <summary>
        /// Create the ship collection
        /// </summary>
        /// <returns>A list of ships</returns>
        IList<Ship> CreateShips();
    }
}
