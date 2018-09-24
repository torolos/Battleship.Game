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
        static void Main()
        {
            var settings = new GameSettings();
            var playerName = Shell.Read("Please enter your name:");
            
            var player = new Player(string.IsNullOrWhiteSpace(playerName)? nameof(Player): playerName, settings);
            var computerPlayer = new ComputerPlayer(new CoordinateHelper(settings), settings);
            var handler = new GameHandler(player, computerPlayer);

            handler.TurnComplete += (s, e) =>
            {
                Shell.Write(e.AttemptResult, s, e.Receiver);
                if (e.AttemptResult.ResultType != ResultType.GameEnds)
                {                   
                    if (e.Receiver is IComputerPlayer)
                    {
                        if (e.AttemptResult.ResultType == ResultType.Used)
                        {
                            WaitForPlayer(handler, true);
                        }
                        PlayerUtil.RenderPlayer(s);
                        handler.ComputerAttempt();
                    }
                    else
                    {
                        WaitForPlayer(handler);
                    }
                }
                else
                {
                    PromptReplay(handler);
                }
            };
            WaitForPlayer(handler);            
        }
       
        static void WaitForPlayer(GameHandler handler, bool retry = false)
        {
            Coordinate coordinate;
            if (!retry)
                Shell.WriteDefault("Enter coordinate:");
            var coor = Console.ReadLine();
            while (!Coordinate.TryParse(coor, out coordinate))
            {
                coor = Shell.Read("Incorrect value, please try again.");
            }
            handler.PlayerAttempt(coordinate);
        } 
        
        static void PromptReplay(GameHandler handler)
        {
            var key = Shell.ReadKey("Press 'n' to start a new game.");
            if (key.KeyChar == 'N' || key.KeyChar == 'n')
            {
                handler.Reset();
                WaitForPlayer(handler);
            }
            else
                Shell.ReadKey("Press any key to close...");
        }
    }
}
