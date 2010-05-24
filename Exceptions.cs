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
        
    	public class InvalidBaseTableException : FormattingException
    	{
            public InvalidBaseTableException () : base() {}
    		public InvalidBaseTableException (string message) : base(message) {}
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