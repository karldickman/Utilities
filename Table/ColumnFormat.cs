using Formatting.Table.Exceptions;
using System;
using System.Collections.Generic;

namespace Formatting.Table
{

    /// <summary>
    /// Describes the column formatting of a table.
    /// </summary>
    public class ColumnFormat
    {
        private List<alignment> alignments;
        private List<char> seperators;
        
        /// <summary>
        /// The alignments of the cells in the column.
        /// </summary>
        public List<alignment> Alignments
        {
            get { return alignments; }
        }
        
        /// <summary>
        /// The seperators between the columns.
        /// </summary>
        public List<char> Seperators
        {
            get { return seperators; }
        }
        
        protected internal ColumnFormat(List<alignment> alignments, List<char> seperators)
        {
            this.alignments = alignments;
            this.seperators = seperators;
        }
    }
    
    public class ColumnFormatFactory
    {
        /// <summary>
        /// Make a new column format with the given alignments and seperators.
        /// </summary>
        public ColumnFormat MakeInstance (List<alignment> alignments, char columnSeperator)
        {
            return MakeInstance (alignments, '\0', columnSeperator, '\0');
        }
        
        /// <summary>
        /// Make a new column format with the given alignments and seperators.
        /// </summary>
        public ColumnFormat MakeInstance (List<alignment> alignments, char leftBorder, char columnSeperator, char rightBorder)
        {
            List<char> seperators = new List<char> ();
            seperators.Add (leftBorder);
            for (int i = 0; i < alignments.Count - 1; i++)
            {
                seperators.Add (columnSeperator);
            }
            seperators.Add (rightBorder);
            return MakeInstance (alignments, seperators);
        }
        
        /// <summary>
        /// Make a new column format with the given alignments and seperators.
        /// </summary>
        public ColumnFormat MakeInstance (List<alignment> alignments, List<char> seperators)
        {
            if (seperators.Count + 1 != alignments.Count)
            {
                throw (new InvalidColumnFormatException ());
            }
            return new ColumnFormat (alignments, seperators);
        }
    }
}
