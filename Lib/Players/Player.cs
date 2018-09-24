using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player: IPlayer
    {
        private ShipList shipList;
        protected readonly IGameSettings gameSettings;   
        /// <summary>
        /// Ctor
        /// </summary>
        public Player(string playerName, IGameSettings gameSettings)
        {
            this.PlayerName = playerName;
            this.gameSettings = gameSettings;
            Init(); 
        }

        #region IPlayer
        public string PlayerName { get; }
        /// <inheritDoc />
        public IDictionary<Coordinate, CoordinateState> OpponentBoard { get; }
            = new Dictionary<Coordinate, CoordinateState>();
        /// <inheritDoc />
        public void Init()
        {
            shipList = new ShipList(gameSettings);
            InitOpponentBoard();
        }
        /// <inheritDoc />
        public void Reset()
        {
            Init();
        }
        /// <inheritDoc />
        public AttemptResult Strike(Coordinate coordinate)
        {
            return shipList.TryReceiveStrike(coordinate);
        }
        /// <inheritDoc />
        public AttemptResult HitOpponent(IPlayer opponent, Coordinate coordinate)
        {
            if (Used().Contains(coordinate))
            {
                return new AttemptResult(ResultType.Used, null, coordinate);
            }
            //Used().Add(coordinate);
            var result = opponent.Strike(coordinate);
            UpdateOpponentBoard(coordinate, result.ResultType);
            return result;
        }
        #endregion

        #region protected
        protected bool CoordinateIsUsed(Coordinate coordinate)
        {
            return OpponentBoard[coordinate] != CoordinateState.None;
        }

        protected IList<Coordinate> Used()
        {
            return OpponentBoard.Where(c => c.Value != CoordinateState.None).Select(c => c.Key).ToList();
        }
        #endregion

        #region private
        private void InitOpponentBoard()
        {
            OpponentBoard.Clear();
            for (var row = 1; row <= gameSettings.BoardSize; row++)
            {
                for(var column = 1; column <= gameSettings.BoardSize; column++)
                {
                    OpponentBoard.Add(new Coordinate(row, column), CoordinateState.None);
                }
            }            
        }

        private void UpdateOpponentBoard(Coordinate coordinate, ResultType resultType)
        {
            var hitTypes = new ResultType[] { ResultType.Hit, ResultType.Sink, ResultType.GameEnds };
            if (hitTypes.Contains(resultType))
            {
                OpponentBoard[coordinate] = CoordinateState.Hit;
            }
            else
            {
                OpponentBoard[coordinate] = CoordinateState.Tried;
            }
        }
        #endregion

    }
}
