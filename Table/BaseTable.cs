using System.Collections.Generic;

namespace Formatting.Table
{    
    
    /// <summary>
    /// A class to format a list of data into columns.
    /// </summary>
    public class BaseTable
    {
        private List<Row> rows;
        
        /// <summary>
        /// The rows in the table
        /// </summary>
        public List<Row> Rows
        {
            get { return rows; }
        }
        
        protected internal BaseTable (List<Row> rows)
        {
            this.rows = rows;
        }
        
        /// <summary>
        /// Render the table.
        /// </summary>
        public List<string> Format ()
        {
            List<string> lines = new List<string> ();
            foreach (Row row in rows)
            {
                lines.Add (row.ToString ());
            }
            return lines;
        }
        
        /// <summary>
        /// Render the table.
        /// </summary>
        public override string ToString ()
        {
            return string.Join ("\n", Format ().ToArray());
        }
    }
}