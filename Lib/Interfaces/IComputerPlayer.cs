using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Interface for computer player
    /// </summary>
    public interface IComputerPlayer: IPlayer
    {
        /// <summary>
        /// Plays a turn for the computer
        /// </summary>
        /// <param name="opponent">The computer's <see cref="IPlayer"/> opponent</param>
        /// <returns>A <see cref="AttemptResult"/> instance/returns>
        AttemptResult AutoPlay(IPlayer opponent);
    }
}
