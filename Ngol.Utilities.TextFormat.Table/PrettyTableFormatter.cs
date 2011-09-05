using System;

namespace TextFormat.Table
{
    /// <summary>
    /// A formatter that produces standard ASCII tables.
    /// </summary>
    public class PrettyTableFormatter : LabeledTableFormatter
    {
        /// <summary>
        /// Create a formatter that produces standard ASCII tables.
        /// </summary>
        public PrettyTableFormatter () : base('|', '|', '|', '-', '-', '-', '-', '+')
        {
        }
    }
}
