using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A collection of <see cref="Column" />s in a <see cref="Table" />.
    /// </summary>
    public class ColumnCollection : IEnumerable<Column>
    {
        #region Properties

        /// <summary>
        /// The number of <see cref="Column" />s in the collectin.
        /// </summary>
        public int Count
        {
            get { return InnerCollection.Count; }
        }

        /// <summary>
        /// The <see cref="Column" />s in the collection.
        /// </summary>
        protected readonly IList<Column> Columns;

        /// <summary>
        /// The <see cref="DataColumnCollection" /> to which to delegate.
        /// </summary>
        protected DataColumnCollection InnerCollection;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="ColumnCollection"/>.
        /// </summary>
        /// <param name="innerCollection">
        /// The <see cref="DataColumnCollection" /> to which to delegate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="innerCollection"/> is <see langword="null" /> .
        /// </exception>
        protected internal ColumnCollection(DataColumnCollection innerCollection)
        {
            if(innerCollection == null)
            {
                throw new ArgumentNullException("innerCollection");
            }
            Columns = new List<Column>();
            InnerCollection = innerCollection;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="Column"/> with the specified <paramref name="columnIndex" />.
        /// </summary>
        /// <param name="columnIndex">
        /// The index of the <see cref="Column" /> to get.
        /// </param>
        public Column this[int columnIndex]
        {
            get { return Columns[columnIndex]; }
        }

        /// <summary>
        /// Add a column to this <see cref="ColumnCollection" />.
        /// </summary>
        /// <param name="columnName">
        /// The name of the <see cref="Column" /> to add.
        /// </param>
        /// <param name="dataType">
        /// The <see cref="Type" /> of the <see cref="Column" /> to add.  Defaults to <see cref="object" />.
        /// </param>
        /// <param name="format">
        /// The method to use to format the <see cref="Column" />.
        /// Defaults to <see cref="object.ToString" />.
        /// </param>
        /// <param name="alignment">
        /// The method to use to align the <see cref="Column" />.
        /// Defaults to <see cref="StringFormatting.LeftJustified" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="columnName"/> is <see langword="null" />.
        /// </exception>
        public void Add(string columnName, Type dataType=null, Format format=null, Alignment alignment=null)
        {
            if(columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }
            if(dataType == null)
            {
                dataType = typeof(object);
            }
            if(format == null)
            {
                format = (val) =>
                {
                    return val != null ? val.ToString() : string.Empty;
                };
            }
            if(alignment == null)
            {
                alignment = StringFormatting.LeftJustified;
            }
            DataColumn dataColumn = new DataColumn(columnName, dataType);
            Column column = new Column(dataColumn, format, alignment);
            InnerCollection.Add(columnName, dataType);
            Columns.Add(column);
        }

        /// <summary>
        /// Remove the <see cref="Column" /> with the specified <paramref name="columnName"/> from this <see cref="ColumnCollection" />.
        /// </summary>
        /// <param name="columnName">
        /// The name of the <see cref="Column" /> to remove.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the <see cref="Column" /> was successfully removed,
        /// <see langword="false" /> if there was no <see cref="Column" /> with the specified
        /// <paramref name="columnName"/>.
        /// </returns>
        public bool Remove(string columnName)
        {
            InnerCollection.Remove(columnName);
            return Columns.Remove(column => column.Name == columnName);
        }

        /// <summary>
        /// Removes the specified <see cref="Column"/> from this <see cref="ColumnCollection" />.
        /// </summary>
        /// <param name="index">
        /// The index of the <see cref="Column" /> to remove.
        /// </param>
        public void RemoveAt(int index)
        {
            InnerCollection.RemoveAt(index);
            Columns.RemoveAt(index);
        }

        #endregion

        #region IEnumerable[Column] implementation

        /// <inheritdoc />
        public IEnumerator<Column> GetEnumerator()
        {
            return Columns.GetEnumerator();
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

