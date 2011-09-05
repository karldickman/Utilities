using System;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A cell containing a string.
    /// </summary>
    public class Cell : ICell
    {
        #region Properties

        /// <summary>
        /// The default alignment for text in the cell.
        /// </summary>
        public static readonly Alignment DefaultAlignment = StringFormatting.LeftJustified;

        /// <summary>
        /// The <see cref="Alignment"/> of the cell.
        /// </summary>
        public Alignment Align
        {
            get;
            set;
        }

        /// <summary>
        /// The text in the rendered cell.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// The value stored in the cell.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a cell with the specified value and the default alignment.
        /// </summary>
        /// <param name="value">
        /// The value to use for the cell.
        /// </param>
        public Cell(object value)
            : this(value, DefaultAlignment)
        {
        }

        /// <summary>
        /// Construct a cell with the specified value and alignment.
        /// </summary>
        /// <param name="value">
        /// The value of the cell.
        /// </param>
        /// <param name="align">
        /// The alignment of the cell.  Use <see langword="null" /> to use the default alignment.
        /// </param>
        public Cell(object value, Alignment align)
        {
            if(align == null)
            {
                align = DefaultAlignment;
            }
            Value = value;
            this.Align = align;
        }

        #endregion

        #region ICell implementation

        /// <inheritdoc />
        public int GetWidth()
        {
            if(Value == null)
            {
                return 0;
            }
            return Value.ToString().Length;
        }

        /// <inheritdoc />
        public void Pad(int width)
        {
            Text = Align(Value, width);
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
