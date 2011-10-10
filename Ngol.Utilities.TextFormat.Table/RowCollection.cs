using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Collection of the <see cref="Row" />s in a <see cref="Table" />.
    /// </summary>
    public class RowCollection : IEnumerable<Row>
    {
        #region Properties

        /// <summary>
        /// The <see cref="DataRowCollection" /> to which many method calls delegate.
        /// </summary>
        protected readonly DataRowCollection InnerCollection;
        /// <summary>
        /// The <see cref="Row" />s in this collection.
        /// </summary>
        protected readonly ICollection<Row> Rows;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="RowCollection"/>.
        /// </summary>
        /// <param name="innerCollection">
        /// The <see cref="DataRowCollection" /> to which to delegate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="innerCollection"/> is <see langword="null" /> .
        /// </exception>
        protected internal RowCollection(DataRowCollection innerCollection)
        {
            if(innerCollection == null)
            {
                throw new ArgumentNullException("innerCollection");
            }
            Rows = new List<Row>();
            InnerCollection = innerCollection;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a <see cref="Row" /> to this collection.
        /// </summary>
        /// <param name="args">
        /// The values in the <see cref="Row"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Row" /> that was added.
        /// </returns>
        public Row Add(params object[] args)
        {
            DataRow dataRow = InnerCollection.Add(args);
            Row row = new Row(dataRow);
            Rows.Add(row);
            return row;
        }

        #endregion

        #region IEnumerable[Row] implementation

        /// <inheritdoc />
        public IEnumerator<Row> GetEnumerator()
        {
            return Rows.GetEnumerator();
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

