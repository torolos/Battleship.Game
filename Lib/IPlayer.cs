using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Interface for game players
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The opponents board state. Used as a template to view which coordinates
        /// have been used by player.
        /// </summary>
        IDictionary<Coordinate, CoordinateState> OpponentBoard { get; }
        /// <summary>
        /// Initialises the player
        /// </summary>
        void Init();
        /// <summary>
        /// Resets player
        /// </summary>
        void Reset();
        /// <summary>
        /// Invoked when player receives strike
        /// </summary>
        /// <param name="coordinate">The <see cref="Coordinate"/> instance the player receives</param>
        /// <returns>A <see cref="AttemptResult"/> object</returns>
        AttemptResult Strike(Coordinate coordinate);
        /// <summary>
        /// Invoked  when the player attempts a strike to the opponent
        /// </summary>
        /// <param name="opponent">The <see cref="IPlayer"/> opponent</param>
        /// <param name="coordinate">The <see cref="Coordinate"/> to try on the opponent</param>
        /// <returns>A<see cref="AttemptResult"/> object</returns>
        AttemptResult HitOpponent(IPlayer opponent, Coordinate coordinate);
    }
}
