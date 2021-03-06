﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PlayerUtil
    {
        public static void RenderPlayer(IPlayer player)
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
            Shell.WriteDefault("");
            Shell.WriteDefault("    1   2   3   4   5   6   7   8   9  10");
            Shell.WriteDefault("  -----------------------------------------");

            var list = new List<Coordinate>();
            for (var i = 1; i <= 10; i++)
            {
                list.AddRange(player.OpponentBoard.Select(c => c.Key).Where(c => c.Row == i).ToList());
                var rowChar = list.First().DisplayName.Substring(0, 1);
                // "A |   |   |   |   |   |   |   |   |   |   |"

                Shell.WriteDefault($"{rowChar} |{string.Join("|", list.Select(c => getStateChar(c))) }|");
                list.Clear();
                Shell.WriteDefault("  -----------------------------------------");
            }
            Shell.WriteDefault("");
        }
    }
}
