using System;

namespace Formatting
{
	
	public class FormattingException : Exception
	{
		public FormattingException (string message) : base(message)
		{
		}
	}
	
	public class InvalidBaseTableException : FormattingException
	{
		public InvalidBaseTableException (string message) : base(message)
		{
		}
	}
}