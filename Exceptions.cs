using System;

namespace Formatting
{
	
	public class FormattingException : Exception
	{
        public FormattingException () : base() {}
		public FormattingException (string message) : base(message)	{}
	}
	
    namespace Table.Exceptions
    {
        
        public class DimensionMismatchException : FormattingException
        {
            public DimensionMismatchException () : base() {}
            public DimensionMismatchException (string message) : base(message) {}
        }
        
    	public class InvalidTableException : FormattingException
    	{
            public InvalidTableException () : base() {}
    		public InvalidTableException (string message) : base(message) {}
    	}
        
        public class InvalidColumnFormatException : FormattingException
        {
            public InvalidColumnFormatException () : base() {}
            public InvalidColumnFormatException(string message) : base(message) {}
        }
        
        public class InvalidRowException : FormattingException
        {
            public InvalidRowException() : base() {}
            public InvalidRowException(string message) : base(message) {}
        }
    }
}