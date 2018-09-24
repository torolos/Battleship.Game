using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Game
{
    public class Shell
    {
        public static void Write(AttemptResult result, IPlayer player, IPlayer receiver)
        {
            switch(result.ResultType)
            {
                case ResultType.Miss:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{player.PlayerName} attempt to {result.Coordinate.DisplayName} ==> {result.ResultType.ToString()}!");
                    break;
                case ResultType.Hit:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{player.PlayerName} hits {receiver.PlayerName} on {result.Coordinate.DisplayName}!");
                    break;
                case ResultType.Sink:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{player.PlayerName} sinks {result.ShipName} of {receiver.PlayerName}!");
                    break;
                case ResultType.GameEnds:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{player.PlayerName} sinks {result.ShipName} of {receiver.PlayerName} and wins game.");
                    break;
                case ResultType.Used:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Coordinate {result.Coordinate.DisplayName} already used, try another one.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    throw new Exception($"Unknown {nameof(ResultType)} value.");
            }
        }

        public static void WriteDefault(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static string Read(string message)
        {
            WriteDefault(message);
            return Console.ReadLine();
        }

        public static ConsoleKeyInfo ReadKey(string message)
        {
            WriteDefault(message);
            return Console.ReadKey();
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
