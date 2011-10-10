using System;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Class that provides metadata on a column in a <see cref="Table" />.
    /// </summary>
    public class Column
    {
        #region Properties

        /// <summary>
        /// The <see cref="Type" /> of this <see cref="Column" />.
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// The method used to format the contens of the <see cref="Column" />.
        /// </summary>
        public Alignment Format
        {
            get { return (object value, int width) => Alignment(InnerFormat(value), width); }
        }

        /// <summary>
        /// The name of this <see cref="Column" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The method used to align the contents of the column.
        /// </summary>
        protected internal Alignment Alignment { get; set; }

        /// <summary>
        /// The method used to format the contens of the column.
        /// </summary>
        protected internal Format InnerFormat { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Column" />.
        /// </summary>
        /// <param name="name">
        /// The name of this <see cref="Column" />
        /// </param>
        /// <param name="dataType">
        /// The <see cref="Type" /> of this <see cref="Column" />
        /// </param>
        /// <param name="alignment">
        /// The <see cref="Alignment" /> to apply to this column.
        /// </param>
        /// <param name="format">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        protected internal Column(string name, Type dataType, Format format, Alignment alignment)
        {
            if(name == null)
            {
                throw new ArgumentNullException("name");
            }
            if(dataType == null)
            {
                throw new ArgumentNullException("dataType");
            }
            if(alignment == null)
            {
                throw new ArgumentNullException("alignment");
            }
            if(format == null)
            {
                throw new ArgumentNullException("format");
            }
            Name = name;
            DataType = dataType;
            Alignment = alignment;
            InnerFormat = format;
        }

        #endregion
    }
}