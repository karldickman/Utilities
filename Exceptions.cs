using System;

namespace TextFormat
{
	/// <summary>
    /// The base exception from which all exceptions derive.
    /// </summary>
	public class FormattingException : Exception
	{
        public FormattingException () : base() {}
		public FormattingException (string message) : base(message)	{}
	}
	
    namespace Table.Exceptions
    {
        /// <summary>
        /// Two arrays or lists that should be the same length are not.
        /// </summary>
        public class DimensionMismatchException : FormattingException
        {
            public DimensionMismatchException () : base() {}
            public DimensionMismatchException (string message)
            : base(message) {}
        }
        
        /// <summary>
        /// The description of used to create table was invalid.
        /// </summary>
    	public class InvalidTableException : FormattingException
    	{
            public InvalidTableException () : base() {}
    		public InvalidTableException (string message) : base(message) {}
    	}
        
        /// <summary>
        /// The description used to create a row was invalid.
        /// </summary>
        public class InvalidRowException : FormattingException
        {
            public InvalidRowException() : base() {}
            public InvalidRowException(string message) : base(message) {}
        }
    }
}