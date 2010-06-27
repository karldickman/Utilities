using System;

using NUnit.Framework;

namespace TextFormat.Table
{    
    /// <summary>
    /// A cell containing a string.
    /// </summary>
    public class Cell : ICell
    {        
        /// <summary>
        /// The default alignment for text in the cell.
        /// </summary>
        protected internal static readonly Alignment DEFAULT_ALIGNMENT = 
            StringFormatting.LeftJustified;

        /// <summary>
        /// The <see cref="Alignment"/> of the cell.
        /// </summary>
        protected internal Alignment Align { get; set; }

        /// <summary>
        /// The text in the rendered cell.
        /// </summary>
        protected internal string Text { get; set; }

        /// <summary>
        /// The value stored in the cell.
        /// </summary>
        protected internal object Value { get; set; }
        
        public Cell (object value_) : this(value_, DEFAULT_ALIGNMENT) {}
        
        public Cell (object value_, Alignment Align)
        {
            if (Align == null)
            {
                Align = DEFAULT_ALIGNMENT;
            }
            Value = value_;
            this.Align = Align;
        }
        
        public void Pad (int width)
        {
            Text = Align (Value, width);
        }
        
        /// <summary>
        /// Render the cell.
        /// </summary>
        override public string ToString ()
        {
            return Text;
        }
        
        /// <summary>
        /// Get the default width of the cell.
        /// </summary>
        public int Width ()
        {
            if (Value == null)
            {
                return 0;
            }     
            return Value.ToString ().Length;
        }
    }
}
