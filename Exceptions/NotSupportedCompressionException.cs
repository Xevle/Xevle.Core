using System;
using System.Collections.Generic;
using System.Text;

namespace Xevle.Core.Exceptions
{
	public class NotSupportedCompressionException : System.Exception
	{
		public NotSupportedCompressionException()
		{
		}

		public NotSupportedCompressionException(string message) : base(message)
		{
		}

		public NotSupportedCompressionException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		protected NotSupportedCompressionException(System.Runtime.Serialization.SerializationInfo info,
		                                           System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}

		~NotSupportedCompressionException()
		{
		}
	}
}
