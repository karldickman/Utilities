using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MoreLinq;
using Ngol.Utilities.Collections.Extensions;
using Ngol.Utilities.System.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A table formatter for tables with one-row headers and footers.
    /// </summary>
    public class LabeledTableFormatter : TableFormatter
    {
        #region Properties

        /// <summary>
        /// The <see cref="string" /> that separates the header
        /// of the table from the body.
        /// </summary>
        public string BodyTop
        {
            get { return BorderCharToString(BodyTopChar); }
        }

        /// <inheritdoc />
        public new bool HasTopBorder
        {
            get { return TopBorderChar != '\0'; }
        }

        /// <summary>
        /// The <see cref="Alignment" /> to apply to the cells in the header.
        /// </summary>
        public readonly Alignment HeaderAlignment;

        /// <inheritdoc />
        public new string TopBorder
        {
            get { return BorderCharToString(TopBorderChar); }
        }

        /// <summary>
        /// The <see cref="char" /> that separates the header
        /// of the table from the body.
        /// </summary>
        protected char BodyTopChar
        {
            get { return base.TopBorderChar; }
        }

        /// <inheritdoc />
        protected new char TopBorderChar { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a formatter that produces tables with lots of decoration.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="char"/> used for the tables' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="char"/> used for the tables' right border.
        /// </param>
        /// <param name="topBorder">
        /// The <see cref="char"/> used for the tables' top border.
        /// </param>
        /// <param name="bottomBorder">
        /// The <see cref="char"/> used for the tables' bottom border.
        /// </param>
        /// <param name="bodyTop">
        /// The <see cref="char"/> that separates the header from the body.
        /// body.
        /// </param>
        /// <param name="corner">
        /// The <see cref="char"/> used at intersections of borders.
        /// </param>
        public LabeledTableFormatter(char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char corner) : base(leftBorder, columnSeparator, rightBorder, bodyTop, bottomBorder, corner)
        {
            TopBorderChar = topBorder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format a <see cref="Table" /> to be pritned on the console.
        /// </summary>
        /// <param name="table">
        /// The sequence of values in the table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public new IEnumerable<string> Format(Table table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return new InnerFormatter(table, LeftBorderChar, ColumnSeparatorChar, RightBorderChar, TopBorderChar, BottomBorderChar, BodyTopChar, CornerChar).Format();
        }

        #endregion

        internal new class InnerFormatter : TableFormatter.InnerFormatter
        {
            #region Properties

            /// <inheritdoc />
            internal new bool HasTopBorder
            {
                get { return TopBorderChar != '\0'; }
            }

            /// <inheritdoc />
            internal new char TopBorderChar { get; set; }

            #endregion

            #region Constructors

            internal InnerFormatter(Table table, char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char corner) : base(table, leftBorder, columnSeparator, rightBorder, bodyTop, bottomBorder, corner)
            {
                ColumnWidths = Table.GetColumnWidths(true);
                TopBorderChar = topBorder;
            }

            #endregion

            #region Methods

            /// <inheritdoc />
            internal new IEnumerable<string> Format()
            {
                if(HasTopBorder)
                {
                    yield return GetRowSeparator(TopBorderChar);
                }
                yield return FormatHeader();
                foreach(string row in base.Format())
                {
                    yield return row;
                }
            }

            /// <summary>
            /// Formats the header of the specified <paramref name="DataTable"/>.
            /// </summary>
            internal string FormatHeader()
            {
                var align = new Func<string, int, string >(Table.HeaderAlignment);
                IList<string > cells = Table.Headers.EquiZip(ColumnWidths, align).ToList();
                return LeftBorder + ColumnSeparator.Join(cells) + RightBorder;
            }

            #endregion
        }
    }
}
