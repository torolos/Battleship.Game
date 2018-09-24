namespace Lib
{
    /// <summary>
    /// A delegate handler firing on completion of a player's turn.
    /// </summary>
    /// <param name="sender">The player that made the attempt</param>
    /// <param name="args">The data wrapper for the result</param>
    public delegate void TurnCompleteEventHandler(IPlayer sender, TurnDataEventArgs args);
}
