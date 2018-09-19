using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Class for a battleship
    /// </summary>
    public class Battleship : Ship
    {
        /// inheritDoc
        public override string Name => nameof(Battleship);
        /// inheritDoc
        public override byte Size => 5;
    }
}
