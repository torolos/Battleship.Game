using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public struct Coordinate
    {
        #region constants
        const int ASCIIconverter = 64;
        const byte MaxRows = 26;
        #endregion

        #region Properties
        /// <summary>
        /// The coordinate row
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// The coordinate column
        /// </summary>
        public int Column { get; }
        /// <summary>
        /// The display name of the coordinate
        /// </summary>
        public string DisplayName { get; }
        #endregion

        #region Ctors
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="row">Th byte value for the row</param>
        /// <param name="column">The byte value for the column</param>  
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
            DisplayName = (char)(Row + ASCIIconverter) + Column.ToString();
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="coordinate">The display name representation of the coordinate</param>
        public Coordinate(string coordinate)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"(^[a-jA-J]{1})(10|[0-9])$");
            if (!regex.Match(coordinate).Success)
            {
                throw new ArgumentException("Invalid coordinate.", nameof(coordinate));
            }
            Row = coordinate.ToUpper().First() - ASCIIconverter;
            Column = Convert.ToInt32(coordinate.Substring(1));
            DisplayName = coordinate.ToUpper();
        }
        #endregion

        #region overrides
        /// <inheritDoc />
        public override string ToString()
        {
            return $"{DisplayName} -> ({Row},{Column})";
        }
        /// <inheritDoc />
        public override bool Equals(object obj)
        {
            return obj is Coordinate && (this == (Coordinate)obj);
        }
        /// <inheritDoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        ///  Equality operator
        /// </summary>
        /// <param name="coordinate1">the first coordinate</param>
        /// <param name="coordinate2">the second coordinate</param>
        /// <returns>true if coordinates are equal</returns>
        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            return coordinate1.Row == coordinate2.Row && coordinate1.Column == coordinate2.Column;
        }
        /// <summary>
        /// Inequality operator
        /// </summary>
        /// <param name="coordinate1">the first coordinate</param>
        /// <param name="coordinate2">the second coordinate</param>
        /// <returns>true if coordinates not equal</returns>
        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return coordinate1.Row != coordinate2.Row || coordinate1.Column != coordinate2.Column;
        }
        #endregion

        #region public
        /// <summary>
        /// Returns the coordinate directly above
        /// </summary>
        /// <param name="coordinate">the new coordinate</param>
        /// <returns>true if coordinate not in ceiling</returns>
        public bool Up(out Coordinate coordinate)
        {
            try
            {
                coordinate = new Coordinate(this.Row - 1, this.Column);
                return true;
            }
            catch (System.ArgumentException)
            {
                coordinate = new Coordinate();
                return false;
            }
        }
        /// <summary>
        /// Return the coordinate directly below
        /// </summary>
        /// <param name="coordinate">the new coordinate</param>
        /// <returns>true if coordinate not in floor</returns>
        public bool Down(out Coordinate coordinate)
        {
            try
            {
                coordinate = new Coordinate(this.Row + 1, this.Column);
                return true;
            }
            catch (System.ArgumentException)
            {
                coordinate = new Coordinate();
                return false;
            }
        }
        /// <summary>
        /// Returns the coordinate directly to the left
        /// </summary>
        /// <param name="coordinate">the new coordinate</param>
        /// <returns>true if coordinate not in outmost left column</returns>
        public bool Left(out Coordinate coordinate)
        {
            try
            {
                coordinate = new Coordinate(this.Row, this.Column - 1);
                return true;
            }
            catch (System.ArgumentException)
            {
                coordinate = new Coordinate();
                return false;
            }
        }
        /// <summary>
        /// Returns the coordinate directly to the right
        /// </summary>
        /// <param name="coordinate">the new coordinate</param>
        /// <returns>true if coordinate not in outmost right</returns>
        public bool Right(out Coordinate coordinate)
        {
            try
            {
                coordinate = new Coordinate(this.Row, this.Column + 1);
                return true;
            }
            catch (System.ArgumentException)
            {
                coordinate = new Coordinate();
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Attempts to convert a string value to a coordinate
        /// </summary>
        /// <param name="coordinateValue">the coordinate value to parse</param>
        /// <param name="coordinate">The <see cref="Coordinate"/> output</param>
        /// <returns>true if parsing successful</returns>
        public static bool TryParse(string coordinateValue, out Coordinate coordinate)
        {
            try
            {
                coordinate = new Coordinate(coordinateValue);
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                coordinate = new Coordinate();
                return false;
            }
        }

    }
}
