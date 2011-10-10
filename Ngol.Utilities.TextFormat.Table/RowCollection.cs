using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Collection of the <see cref="Row" />s in a <see cref="Table" />.
    /// </summary>
    public class RowCollection : IEnumerable<Row>
    {
        #region Properties

        /// <summary>
        /// The number of <see cref="Column" />s in the owning table.
        /// </summary>
        protected int ColumnCount
        {
            get { return Table.ColumnCount; }
        }

        /// <summary>
        /// The <see cref="Row" />s in this collection.
        /// </summary>
        protected readonly ICollection<Row> Rows;

        /// <summary>
        /// The <see cref="Table" /> that owns this <see cref="RowCollection" />.
        /// </summary>
        protected readonly Table Table;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="RowCollection"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> is <see langword="null" />.
        /// </exception>
        protected internal RowCollection(Table table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            Table = table;
            Rows = new List<Row>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a <see cref="Row" /> to this collection.
        /// </summary>
        /// <param name="values">
        /// The values in the <see cref="Row"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Row" /> that was added.
        /// </returns>
        public Row Add(params object[] values)
        {
            IEnumerable<object > rowValues;
            if(values.Length > ColumnCount)
            {
                rowValues = values.Take(ColumnCount);
            }
            else if(values.Length < ColumnCount)
            {
                IList<object > list = values.ToList();
                for(int i = 0; i < ColumnCount - values.Length; i++)
                {
                    list.Add(null);
                }
                rowValues = list;
            }
            else
            {
                rowValues = values;
            }
            Row row = new Row(rowValues);
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

