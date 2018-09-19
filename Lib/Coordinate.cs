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
            if (row > GameUtility.BOARD_SIZE || column > GameUtility.BOARD_SIZE
                || row == 0 || column == 0)
            {
                throw new ArgumentException("You cannot have more than 26 rows", nameof(row));
            }
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
        public override string ToString()
        {
            return $"{DisplayName} -> ({Row},{Column})";
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate && (this == (Coordinate)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            return coordinate1.Row == coordinate2.Row && coordinate1.Column == coordinate2.Column;
        }
        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return coordinate1.Row != coordinate2.Row || coordinate1.Column != coordinate2.Column;
        }
        #endregion
    }
}
