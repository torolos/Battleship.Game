using System.Collections.Generic;
namespace Lib
{
    public interface ICoordinateUtility
    {
        /// <summary>
        /// Returns the direct adjacents of the specified coordinate
        /// </summary>
        /// <param name="coordinate">The specified coordinate</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="Coordinate"/></returns>
        IList<Coordinate> GetAdjacent(Coordinate coordinate);
        /// <summary>
        /// Returns the possible on same line adjacents from a list of coordinates
        /// This serves as a calculator for the computer to continue from a successful
        /// strike
        /// </summary>
        /// <param name="coordinates">The successful coordinates</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="Coordinate"/></returns>
        IList<Coordinate> GetAdjacent(IList<Coordinate> coordinates);
        /// <summary>
        /// Returns the possible 'on same line' adjacents from a list of coordinates.
        /// </summary>
        /// <param name="coordinates">The successful coordinates</param>
        /// <param name="exclusions">The coordinates to exclude</param>
        /// <param name="result">The resulting coordinates</param>
        /// <returns>True if there are available adjacent coordinates</returns>
        bool TryGetAdjacent(IList<Coordinate> coordinates,
            IList<Coordinate> exclusions, out IList<Coordinate> result);
    }
}