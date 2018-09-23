using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class GameHandler
    {
        private readonly IPlayer player;
        private readonly IComputerPlayer computerPlayer;

        public event TurnCompleteEventHandler TurnComplete;

        public GameHandler(IPlayer player, IComputerPlayer computerPlayer)
        {
            this.player = player;
            this.computerPlayer = computerPlayer;
            Init();
        }

        public AttemptResult PlayerAttempt(Coordinate coordinate)
        {
            return player.HitOpponent(computerPlayer, coordinate);
        }

        public AttemptResult ComputerAttempt()
        {
            return computerPlayer.AutoPlay(player);
        }

        public void Reset()
        {
            Init();
        }

        private void Init()
        {
            player.Reset();
            computerPlayer.Reset();
        }

    }
    /// <summary>
    /// A delegate handler firing on completion of a player's turn.
    /// </summary>
    /// <param name="sender">The player that made the attempt</param>
    /// <param name="args">The data wrapper for the result</param>
    public delegate void TurnCompleteEventHandler(IPlayer sender, TurnDataEventArgs args);

    public class TurnDataEventArgs: EventArgs
    {
        public AttemptResult AttemptResult { get; }

        public TurnDataEventArgs(AttemptResult attemptResult)
        {
            AttemptResult = attemptResult;
        }
    }
}
