using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A row in a table.
    /// </summary>
    public class Row
    {
        #region Properties

        /// <summary>
        /// The cells in the row.
        /// </summary>
        public IEnumerable<ICell> Cells
        {
            get;
            protected set;
        }

        /// <summary>
        /// The separators between the cells.
        /// </summary>
        public IEnumerable<char> Separators
        {
            get;
            protected set;
        }

        /// <summary>
        /// The number of columns in the table.
        /// </summary>
        public int ColumnCount
        {
            get { return Cells.Count(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new row.
        /// </summary>
        /// <param name="cells">
        /// The cells in the row.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public Row(IEnumerable<ICell> cells) : this(cells, Enumerable.Repeat<char>('\0', cells.Count()))
        {
        }

        /// <summary>
        /// Create a new row.
        /// </summary>
        /// <param name="cells">
        /// The cells in the row.
        /// </param>
        /// <param name="separators">
        /// The separators between the cells.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public Row(IEnumerable<ICell> cells, IEnumerable<char> separators)
        {
            if(cells == null)
                throw new ArgumentNullException("cells");
            if(separators == null)
                throw new ArgumentNullException("separators");
            // Convert to list to avoid odd behaviors
            Cells = cells.ToList();
            Separators = separators;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the widths of all the columns.
        /// </summary>
        public IEnumerable<int> GetColumnWidths()
        {
            return Cells.Select(cell => cell.GetWidth());
        }

        /// <summary>
        /// Pad all cells in the row to the given widths.
        /// </summary>
        /// <exception cname="ArgumentException">
        /// Raised when more cells are provided than widths.
        /// </exception>
        public void Pad(IEnumerable<int> widths)
        {
            if(widths == null)
                throw new ArgumentNullException("widths");
            if(Cells.Count() > widths.Count())
                throw new ArgumentException("A width must be specified for every cell.");
            Cells.ForEachEqual(widths.Take(ColumnCount), (cell, width) => { cell.Pad(width); });
        }

        #endregion

        #region Inherited methods

        /// <summary>
        /// Format the row.
        /// </summary>
        public override string ToString()
        {
            Debug.Assert(Cells.Count() + 1 == Separators.Count());
            string result = "";
            Cells.ForEach(Separators, (cell, separator) =>
            {
                if(separator != '\0')
                {
                    result += separator;
                }
                result += cell;
            });
            if(Separators.Last() != '\0')
            {
                result += Separators.Last();
            }
            return result;
        }
        
        #endregion
    }
}
