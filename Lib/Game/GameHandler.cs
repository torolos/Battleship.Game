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
        /// <summary>
        /// The event handler for the event fired when a player
        /// completes his turn.
        /// </summary>
        public event TurnCompleteEventHandler TurnComplete;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="player">The 'human' player</param>
        /// <param name="computerPlayer">the computer opponentparam>
        public GameHandler(IPlayer player, IComputerPlayer computerPlayer)
        {
            this.player = player;
            this.computerPlayer = computerPlayer;
            Init();
        }
        /// <summary>
        /// Attempt by 'human' player
        /// </summary>
        /// <param name="coordinate">The coordinate used</param>
        public void PlayerAttempt(Coordinate coordinate)
        {
            var result = player.HitOpponent(computerPlayer, coordinate);
            OnPlayerTurnComplete(result, player, computerPlayer);
        }
        /// <summary>
        /// Attempt by computer player
        /// </summary>
        public void ComputerAttempt()
        {
            var result = computerPlayer.AutoPlay(player);
            OnPlayerTurnComplete(result, computerPlayer, player);
        }
        /// <summary>
        /// Resets handler
        /// </summary>
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
}
