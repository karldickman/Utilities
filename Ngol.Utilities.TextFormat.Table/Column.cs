using System;
using System.Data;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Class that provides metadata on a column in a <see cref="Table" />.
    /// </summary>
    public class Column
    {
        #region Properties

        /// <summary>
        /// The method used to format the contens of the column.
        /// </summary>
        public Alignment Format
        {
            get { return (object value, int width) => Alignment(InnerFormat(value), width); }
        }

        /// <summary>
        /// The name of this column.
        /// </summary>
        public string Name
        {
            get { return InnerColumn.ColumnName; }
            set { InnerColumn.ColumnName = value; }
        }

        /// <summary>
        /// The method used to align the contents of the column.
        /// </summary>
        protected internal Alignment Alignment { get; set; }

        /// <summary>
        /// The method used to format the contens of the column.
        /// </summary>
        protected internal Format InnerFormat { get; set; }

        /// <summary>
        /// The <see cref="DataColumn" /> to which many method calls are delegated.
        /// </summary>
        protected readonly DataColumn InnerColumn;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Column" />.
        /// </summary>
        /// <param name="innerColumn">
        /// The <see cref="DataColumn" /> to which to delegate.
        /// </param>
        /// <param name="alignment">
        /// The <see cref="Alignment" /> to apply to this column.
        /// </param>
        /// <param name="format">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        protected internal Column(DataColumn innerColumn, Format format, Alignment alignment)
        {
            if(innerColumn == null)
            {
                throw new ArgumentNullException("innerColumn");
            }
            if(alignment == null)
            {
                throw new ArgumentNullException("alignment");
            }
            if(format == null)
            {
                throw new ArgumentNullException("format");
            }
            Alignment = alignment;
            InnerFormat = format;
            InnerColumn = innerColumn;
        }

        #endregion
    }
}