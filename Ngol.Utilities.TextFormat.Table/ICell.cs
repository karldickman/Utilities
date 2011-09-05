using System;

namespace TextFormat
{
    /// <summary>
    /// The interface that all cells of the table must implement.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Returns the cell padded to the given width.
        /// </summary>
        void Pad (int width);

        /// <summary>
        /// Get the width required for the data in the cell.
        /// </summary>
        int Width ();
    }
}
