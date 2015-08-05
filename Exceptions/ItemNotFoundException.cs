using System;
using System.Collections.Generic;
using System.Text;

namespace Xevle.Core.Exceptions
{
	public class ItemNotFoundException : System.Exception
	{
		public ItemNotFoundException()
		{
		}

		public ItemNotFoundException(string message) : base(message)
		{
		}

		public ItemNotFoundException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		protected ItemNotFoundException(System.Runtime.Serialization.SerializationInfo info,
		                                System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}

		~ItemNotFoundException()
		{
		}
	}
}
