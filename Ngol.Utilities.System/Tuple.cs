using System;
using System.Collections.Generic;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.System
{
    /// <summary>
    /// Provides static methods for creating tuple objects, and access to the only zero tuple.
    /// </summary>
    public class Tuple : IComparable
    {
        #region Properties

        #region Physical implementation

        private static Tuple _zeroTuple;

        #endregion

        /// <summary>
        /// The only instance of the zero-tuple.
        /// </summary>
        public static Tuple ZeroTuple
        {
            get
            {
                if(_zeroTuple == null)
                {
                    _zeroTuple = new Tuple();
                }
                return _zeroTuple;
            }
        }

        #endregion

        #region Constructors

        private Tuple()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new 1-tuple or singleton.
        /// </summary>
        /// <param name="value">
        /// The first item in the tuple.
        /// </param>
        public static Tuple<T1> Create<T1>(T1 value)
        {
            return new Tuple<T1>(value);
        }

        /// <summary>
        /// Create a new 2-tuple or pair.
        /// </summary>
        /// <param name="value1">
        /// The first item in the tuple.
        /// </param>
        /// <param name="value2">
        /// The second item in the tuple.
        /// </param>
        public static Tuple<T1, T2> Create<T1, T2>(T1 value1, T2 value2)
        {
            return new Tuple<T1, T2>(value1, value2);
        }

        /// <summary>
        /// Create a new 3-tuple or triple.
        /// </summary>
        /// <param name="value1">
        /// The first item in the tuple.
        /// </param>
        /// <param name="value2">
        /// The second item in the tuple.
        /// </param>
        /// <param name="value3">
        /// The third item in the tuple.
        /// </param>
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new Tuple<T1, T2, T3>(value1, value2, value3);
        }

        /// <summary>
        /// The == operator cannot be applied directly to type variables,
        /// so we have to define a utility method to check their equality.
        /// </summary>
        internal static bool ItemsEqual<T>(T item1, T item2)
        {
            if(ReferenceEquals(item1, item2))
            {
                return true;
            }
            if(item1 == null || item2 == null)
            {
                return false;
            }
            return item1.Equals(item2);
        }

        #endregion

        #region Inherited methods

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            return this == other;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public string GetString()
        {
            return "()";
        }

        #endregion

        #region IComparable implementation

        int IComparable.CompareTo(object obj)
        {
            Tuple that = obj as Tuple;
            if(that == null)
            {
                throw new ArgumentException("Expected object of type Tuple.");
            }
            // There is only one instance.
            return 0;
        }

        #endregion
    }

    /// <summary>
    /// Represents a 1-tuple or singleton.
    /// </summary>
    public class Tuple<T1> : IComparable
    {
        #region Properties

        /// <summary>
        /// The tuple's only component.
        /// </summary>
        public readonly T1 Item1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new tuple.
        /// </summary>
        /// <param name="item1">
        /// The value of the tuple's only item.
        /// </param>
        public Tuple(T1 item1)
        {
            Item1 = item1;
        }
        
        #endregion

        #region Inherited methods

        /// <summary>
        /// Overload of == operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator ==(Tuple<T1> tuple1, Tuple<T1> tuple2)
        {
            if(ReferenceEquals(tuple1, tuple2))
            {
                return true;
            }
            if(ReferenceEquals(tuple1, null) || ReferenceEquals(tuple2, null))
            {
                return false;
            }
            return tuple1.Equals(tuple2);
        }

        /// <summary>
        /// Overload of != operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator !=(Tuple<T1> tuple1, Tuple<T1> tuple2)
        {
            return !(tuple1 == tuple2);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other as Tuple<T1>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("({0})", Item1);
        }

        /// <summary>
        /// Determine the equality of two tuples.
        /// </summary>
        protected bool Equals(Tuple<T1> that)
        {
            if(ReferenceEquals(that, null))
            {
                return false;
            }
            if(ReferenceEquals(this, that))
            {
                return true;
            }
            return Tuple.ItemsEqual<T1>(Item1, that.Item1);
        }

        #endregion

        #region IComparable implementation

        int IComparable.CompareTo(object other)
        {
            Tuple<T1> that = other as Tuple<T1>;
            if(that == null)
            {
                throw new ArgumentException("Expected object of type Tuple<T1>.");
            }
            Comparer<T1> comparer = Comparer<T1>.Default;
            return comparer.Compare(Item1, that.Item1);
        }

        #endregion
    }

    /// <summary>
    /// Represents a 2-tuple or pair.
    /// </summary>
    public class Tuple<T1, T2> : IComparable
    {
        #region Properties

        /// <summary>
        /// The tuple's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The tuple's second component.
        /// </summary>
        public readonly T2 Item2;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new tuple.
        /// </summary>
        /// <param name="item1">
        /// The value of the tuple's first item.
        /// </param>
        /// <param name="item2">
        /// The value of the tuple's first item.
        /// </param>
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        #endregion

        #region Inherited methods

        /// <summary>
        /// Overload of == operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator ==(Tuple<T1, T2> tuple1, Tuple<T1, T2> tuple2)
        {
            if(ReferenceEquals(tuple1, tuple2))
            {
                return true;
            }
            if(ReferenceEquals(tuple1, null) || ReferenceEquals(tuple2, null))
            {
                return false;
            }
            return tuple1.Equals(tuple2);
        }

        /// <summary>
        /// Overload of != operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator !=(Tuple<T1, T2> tuple1, Tuple<T1, T2> tuple2)
        {
            return !(tuple1 == tuple2);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other as Tuple<T1, T2>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("({0}, {1})", Item1, Item2);
        }

        /// <summary>
        /// Determine the equality of two tuples.
        /// </summary>
        protected bool Equals(Tuple<T1, T2> that)
        {
            if(ReferenceEquals(that, null))
            {
                return false;
            }
            if(ReferenceEquals(this, that))
            {
                return true;
            }
            return Tuple.ItemsEqual(Item1, that.Item1) && Tuple.ItemsEqual(Item2, that.Item2);
        }

        #endregion

        #region IComparable implementation

        int IComparable.CompareTo(object other)
        {
            Tuple<T1, T2> that = other as Tuple<T1, T2>;
            if(that == null)
            {
                throw new ArgumentNullException("Expected object of type Tuple<T1, T2>");
            }
            int comparison = Comparer<T1>.Default.Compare(Item1, that.Item1);
            if(comparison != 0)
            {
                return comparison;
            }
            return Comparer<T2>.Default.Compare(Item2, that.Item2);
        }
        
        #endregion
    }

    /// <summary>
    /// Represents a 3-tuple or triple.
    /// </summary>
    public class Tuple<T1, T2, T3> : IComparable
    {
        #region Properties

        /// <summary>
        /// The tuple's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The tuple's second component.
        /// </summary>
        public readonly T2 Item2;

        /// <summary>
        /// The tuple's third component.
        /// </summary>
        public readonly T3 Item3;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new tuple.
        /// </summary>
        /// <param name="item1">
        /// The value of the tuple's first item.
        /// </param>
        /// <param name="item2">
        /// The value of the tuple's first item.
        /// </param>
        /// <param name="item3">
        /// The value of the tuple's first item.
        /// </param>
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        #endregion

        #region Inherited methods

        /// <summary>
        /// Overload of == operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator ==(Tuple<T1, T2, T3> tuple1, Tuple<T1, T2, T3> tuple2)
        {
            if(ReferenceEquals(tuple1, tuple2))
            {
                return true;
            }
            if(ReferenceEquals(tuple1, null) || ReferenceEquals(tuple2, null))
            {
                return false;
            }
            return tuple1.Equals(tuple2);
        }

        /// <summary>
        /// Overload of != operator that delegates to <see cref="Equals(object)" />.
        /// </summary>
        public static bool operator !=(Tuple<T1, T2, T3> tuple1, Tuple<T1, T2, T3> tuple2)
        {
            return !(tuple1 == tuple2);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other as Tuple<T1, T2, T3>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}", Item1, Item2, Item3);
        }

        /// <summary>
        /// Determine the equality of two tuples.
        /// </summary>
        protected bool Equals(Tuple<T1, T2, T3> that)
        {
            if(ReferenceEquals(that, null))
            {
                return false;
            }
            if(ReferenceEquals(this, that))
            {
                return true;
            }
            return Tuple.ItemsEqual(Item1, that.Item1) && Tuple.ItemsEqual(Item2, that.Item2) && Tuple.ItemsEqual(Item3, that.Item3);
        }

        #endregion

        #region IComparable implementation

        int IComparable.CompareTo(object other)
        {
            Tuple<T1, T2, T3> that = other as Tuple<T1, T2, T3>;
            if(that == null)
            {
                throw new ArgumentNullException("Expected object of type Tuple<T1, T2, T3>");
            }
            int comparison = Comparer<T1>.Default.Compare(Item1, that.Item1);
            if(comparison != 0)
            {
                return comparison;
            }
            comparison = Comparer<T2>.Default.Compare(Item2, that.Item2);
            if(comparison != 0)
            {
                return comparison;
            }
            return comparison = Comparer<T3>.Default.Compare(Item3, that.Item3);
        }

        #endregion
    }
}

