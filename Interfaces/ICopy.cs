using System;

namespace Xevle.Core
{
	/// <summary>
	/// Interface for a generic copy method
	/// </summary>
	public interface ICopy<T>
	{
		/// <summary>
		/// Copy the instance of T and with all internals.
		/// </summary>
		T Copy();
	}
}

