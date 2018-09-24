using System;
using System.Threading;
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

        public void PlayerAttempt(Coordinate coordinate)
        {
            var result = player.HitOpponent(computerPlayer, coordinate);
            OnPlayerTurnComplete(result, player, computerPlayer);
        }

        public void ComputerAttempt()
        {
            var result = computerPlayer.AutoPlay(player);
            OnPlayerTurnComplete(result, computerPlayer, player);
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

        private void OnPlayerTurnComplete(AttemptResult result, IPlayer actor, IPlayer next)
        {
            Volatile.Read(ref TurnComplete)?.Invoke(actor, new TurnDataEventArgs(result, next));
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
