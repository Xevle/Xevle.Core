using System;
using System.Collections.Generic;
using System.Text;

namespace Xevle.Core.Generics
{
	/// <summary>
	/// A generic Pair of two instances of the same type.
	/// </summary>
	public class Pair<T>
	{
		/// <summary>
		/// The first instance of the pair.
		/// </summary>
		public T First;

		/// <summary>
		/// The second instance of the pair.
		/// </summary>
		public T Second;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Pair`1"/> class.
		/// </summary>
		public Pair() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Pair`1"/> class.
		/// </summary>
		/// <param name="first">First instance</param>
		/// <param name="second">Second instance</param>
		public Pair(T first, T second)
		{
			First=first;
			Second=second;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Xevle.Pair`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Xevle.Pair`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Xevle.Pair`1"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if(obj==null) return false;

			Pair<T> o=(Pair<T>)obj;
			return First.Equals(o.First)&&Second.Equals(o.Second);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Xevle.Pair`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			if((object)First==null)
			{
				if((object)Second==null) return 0;
				return (-1)^Second.GetHashCode();
			}

			if((object)Second==null) return First.GetHashCode();
			return First.GetHashCode()^Second.GetHashCode();
		}
	}
}
