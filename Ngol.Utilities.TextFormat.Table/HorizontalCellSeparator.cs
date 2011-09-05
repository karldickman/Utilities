using System;

namespace TextFormat.Table
{
    /// <summary>
    /// The horizontal separator between two cells.
    /// </summary>
    public class HorizontalCellSeparator : ICell
    {
        /// <summary>
        /// The character used to seperate rows.
        /// </summary>
        protected internal char Separator { get; set; }

        /// <summary>
        /// The text of the cell.
        /// </summary>
        protected internal string Text { get; set; }

        public HorizontalCellSeparator (char separator)
        {
            Separator = separator;
        }
        
        public void Pad (int width)
        {
            Text = "";
            for (int i = 0; i < width; i++)
            {
                Text += Separator;
            }
        }
        
        /// <summary>
        /// Render the cell.
        /// </summary>
        override public string ToString ()
        {
            return Text;
        }
        
        public int Width ()
        {
            return 0;
        }
    }
}
