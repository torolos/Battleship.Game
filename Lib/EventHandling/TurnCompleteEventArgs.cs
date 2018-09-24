using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Event args class for each player's turn
    /// </summary>
    public class TurnDataEventArgs : EventArgs
    {
        /// <summary>
        /// The result of the attempt
        /// </summary>
        public AttemptResult AttemptResult { get; }
        /// <summary>
        /// The receiver of the turn
        /// </summary>
        public IPlayer Receiver { get; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="attemptResult">The result of the attempt</param>
        /// <param name="actor">The player that just completed his turn</param>
        /// <param name="receiver">The receiver</param>
        public TurnDataEventArgs(AttemptResult attemptResult, IPlayer receiver)
        {
            AttemptResult = attemptResult;
            Receiver = receiver;
        }
    }
}
