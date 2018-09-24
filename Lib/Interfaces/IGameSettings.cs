using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Interface for game settings
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// The size of the game board
        /// </summary>
        int BoardSize { get; }
    }
}
