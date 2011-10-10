using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Class to represent a row in a <see cref="Table" />.
    /// </summary>
    public class Row : IEnumerable<object>
    {
        #region Properties

        /// <summary>
        /// The <see cref="DataRow" /> to which most method calls are delegated.
        /// </summary>
        protected readonly DataRow InnerRow;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Row" />.
        /// </summary>
        /// <param name="row">
        /// The <see cref="DataRow" /> to which to delegate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="row"/> is <see langword="null" />.
        /// </exception>
        protected internal Row(DataRow row)
        {
            if(row == null)
            {
                throw new ArgumentNullException("row");
            }
            InnerRow = row;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets or sets the value in this <see cref="Row"/> at the specified <paramref name="columnIndex"/>.
        /// </summary>
        /// <param name="columnIndex">
        /// The index of the <see cref="Column" /> whose value to set.
        /// </param>
        public object this[int columnIndex]
        {
            get { return InnerRow.ItemArray[columnIndex]; }

            set { InnerRow.ItemArray[columnIndex] = value; }
        }
        
        #endregion

        #region IEnumerable[object] implementation

        /// <inheritdoc />
        public IEnumerator<object> GetEnumerator()
        {
            return InnerRow.ItemArray.Cast<object>().GetEnumerator();
        }

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #endregion
    }
}

