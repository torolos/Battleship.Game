using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Class for a destroyer ship
    /// </summary>
    public class Destroyer : Ship
    {
        private string name;
        ///inheritDoc
        public override string Name => name;
        ///inheritDoc
        public override byte Size => 4;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">The name of the destoryer</param>
        public Destroyer(string name)
        {
            this.name = name;
        }
    }
}
