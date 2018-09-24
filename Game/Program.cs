using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Game
{
    class Program
    {
        static GameHandler handler;
        static void Main(string[] args)
        {
            var settings = new GameSettings();
            Console.WriteLine("Please enter your name:");
            var playerName = Console.ReadLine();
            
            var player = new Player(string.IsNullOrWhiteSpace(playerName)? nameof(Player): playerName, settings);
            var computerPlayer = new ComputerPlayer(new CoordinateHelper(settings), settings);
            handler = new GameHandler(player, computerPlayer);
            var writer = new AttemptResultWriter();
            handler.TurnComplete += (s, e) =>
            {
                writer.Write(e.AttemptResult, s, e.Receiver);
                if (e.AttemptResult.ResultType != ResultType.GameEnds)
                {
                    if (e.Receiver is IComputerPlayer)
                    {
                        RenderPlayer(s);
                        handler.ComputerAttempt();
                    }
                    else
                    {
                        WaitForPlayer();
                    }
                }
            };
            WaitForPlayer();
            Console.ReadKey();
        }

        
        static void WaitForPlayer()
        {
            Coordinate coordinate;
            Console.WriteLine("Enter coordinate:");
            var coor = Console.ReadLine();
            while (!Coordinate.TryParse(coor, out coordinate))
            {
                Console.WriteLine("Incorrect value, please try again.");
                coor = Console.ReadLine();
            }
            handler.PlayerAttempt(coordinate);
        }

        static bool WaitForComputer()
        {
            throw new NotImplementedException();
        }

        static void PromptForReplay()
        {

        }



        #region test
        static void TestRender(IPlayer player)
        {
            var counter = 0;

            var used = new List<Coordinate>();
            while (counter < 10)
            {
                var coor = GameUtility.CreateRandomCoordinate();
                player.OpponentBoard[coor] = CoordinateState.Hit;
                used.Add(coor);
                counter += 1;
            }
            counter = 0;
            while (counter < 10)
            {
                var coor = GameUtility.CreateRandomCoordinate(used);
                player.OpponentBoard[coor] = CoordinateState.Tried;
                used.Add(coor);
                counter += 1;
            }
            RenderPlayer(player);
        }
        #endregion
        static void RenderPlayer(IPlayer player)
        {
            string getStateChar(Coordinate coordinate)
            {
                var state = player.OpponentBoard[coordinate];
                switch (state)
                {
                    case CoordinateState.Hit:
                        return " o ";
                    case CoordinateState.Tried:
                        return " x ";
                    default:
                        return "   ";
                }
            }

            Console.WriteLine("    1   2   3   4   5   6   7   8   9  10");
            Console.WriteLine("  -----------------------------------------");

            var list = new List<Coordinate>();
            for (var i = 1; i <= 10; i++)
            {
                list.AddRange(player.OpponentBoard.Select(c => c.Key).Where(c => c.Row == i).ToList());
                var rowChar = list.First().DisplayName.Substring(0, 1);
                // "A |   |   |   |   |   |   |   |   |   |   |"

                Console.WriteLine($"{rowChar} |{string.Join("|", list.Select(c => getStateChar(c))) }|");
                list.Clear();
                Console.WriteLine("  -----------------------------------------");
            }          
        }
    }
}
