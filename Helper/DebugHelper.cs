using System;
using System.Diagnostics;

namespace Xevle.Core.Helper
{
	public static class DebugHelper
	{
		[Conditional("DEBUG")]
		public static void Break(bool @break = false)
		{
			if (Debugger.IsAttached == true)
			{
				if (@break) Debugger.Break();
			}
		}

		[Conditional("DEBUG")]
		public static void BreakOne(bool @break = false)
		{
			Break(@break);
		}

		[Conditional("DEBUG")]
		public static void BreakTwo(bool @break = false)
		{
			Break(@break);
		}

		[Conditional("DEBUG")]
		public static void BreakThree(bool @break = false)
		{
			Break(@break);
		}
	}
}

