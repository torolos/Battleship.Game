using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Game
{
    public class AttemptResultWriter
    {
        public void Write(AttemptResult result, IPlayer player, IPlayer receiver)
        {
            switch(result.ResultType)
            {
                case ResultType.Miss:
                case ResultType.Hit:
                    Console.WriteLine($"{player.PlayerName} attemp to {result.Coordinate.DisplayName} -> {result.ResultType.ToString()}");
                    break;
                case ResultType.Sink:
                case ResultType.GameEnds:
                    Console.WriteLine($"{player.PlayerName} sinks {result.ShipName} of {receiver.PlayerName}!");
                    break;
                case ResultType.Used:
                    Console.WriteLine($"Coordinate {result.Coordinate.DisplayName} already used, try another one.");
                    break;
                default:
                    throw new Exception($"Unknown {nameof(ResultType)} value.");
            }
        }
    }
}
