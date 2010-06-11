using System;
using System.Runtime.Serialization;

namespace TextFormat
{
	/// <summary>
    /// The base exception from which all exceptions derive.
    /// </summary>
	public class TextFormatException : Exception
	{
        public TextFormatException () : base() {}
		public TextFormatException (string message) : base(message)	{}
        public TextFormatException(string message, Exception exception)
        : base(message, exception) {}
        protected TextFormatException(SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
	
    namespace Table
    {
        /// <summary>
        /// Two arrays or lists that should be the same length are not.
        /// </summary>
        public class DimensionMismatchException : TextFormatException
        {
            public DimensionMismatchException () : base() {}
            public DimensionMismatchException (string message)
            : base(message) {}
            public DimensionMismatchException (string message,
                Exception exception) : base(message, exception) {}
            protected DimensionMismatchException(SerializationInfo info,
                StreamingContext context) : base(info, context) {}
        }
    }
}