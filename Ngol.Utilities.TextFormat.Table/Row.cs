using System;
using System.Collections;
using System.Collections.Generic;
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
        /// The values in this <see cref="Row" />.
        /// </summary>
        protected readonly object[] Values;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Row"/>.
        /// </summary>
        /// <param name="values">
        /// The values to put in the <see cref="Row" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        protected internal Row(IEnumerable<object> values)
        {
            if(values == null)
            {
                throw new ArgumentNullException("values");
            }
            Values = values.ToArray();
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
            get
            {
                try
                {
                    return Values[columnIndex];
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

            set { Values[columnIndex] = value; }
        }
        
        #endregion

        #region IEnumerable[object] implementation

        /// <inheritdoc />
        public IEnumerator<object> GetEnumerator()
        {
            return Values.Cast<object>().GetEnumerator();
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

