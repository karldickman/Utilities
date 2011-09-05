using System;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// The interface that all cells of the table must implement.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Get the width required for the data in the cell.
        /// </summary>
        int GetWidth();

        /// <summary>
        /// Returns the cell padded to the given width.
        /// </summary>
        void Pad(int width);
    }
}
