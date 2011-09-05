using System;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// The horizontal separator between two cells.
    /// </summary>
    public class HorizontalCellSeparator : ICell
    {
        #region Properties

        /// <summary>
        /// The character used to seperate rows.
        /// </summary>
        public char Separator
        {
            get;
            set;
        }

        /// <summary>
        /// The text of the cell.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new seperator.
        /// </summary>
        /// <param name="separator">
        /// The <see cref="char"/> to use to build the separator.
        /// </param>
        public HorizontalCellSeparator(char separator)
        {
            Separator = separator;
        }

        #endregion

        #region ICell implementation

        /// <inheritdoc />
        public int GetWidth()
        {
            return 0;
        }

        /// <inheritdoc />
        public void Pad(int width)
        {
            Text = "";
            for(int i = 0; i < width; i++)
            {
                Text += Separator;
            }
        }

        #endregion

        #region Inherited methods

        /// <inheritdoc />
        public override string ToString()
        {
            return Text;
        }

        #endregion
    }
}
