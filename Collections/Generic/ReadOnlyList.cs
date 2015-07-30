using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xevle.Core.Collections.Generic
{
	/// <summary>
	/// Read only list.
	/// </summary>
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	public class ReadOnlyList<T> : IList<T>, IList, IReadOnlyList<T>
	{
		/// <summary>
		/// The list.
		/// </summary>
		protected IList<T> list;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.ReadOnlyList`1"/> class.
		/// </summary>
		public ReadOnlyList()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.ReadOnlyList`1"/> class.
		/// </summary>
		/// <param name="collection">Collection.</param>
		public ReadOnlyList(IEnumerable<T> collection)
		{
			list = new List<T>(collection);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.ReadOnlyList`1"/> class.
		/// </summary>
		/// <param name="collection">Collection.</param>
		public ReadOnlyList(params T[] collection)
		{
			list = new List<T>(collection);
		}

		void ThrowReadOnly()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		#region Implements IList<T>
		/// <summary>
		/// Determines the index of a specific item in the current instance.
		/// </summary>
		/// <returns>The of.</returns>
		/// <param name="item">Item.</param>
		public int IndexOf(T item)
		{
			return list.IndexOf(item);
		}

		/// <summary>
		/// Insert the specified index and item.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="item">Item.</param>
		public void Insert(int index, T item)
		{
			ThrowReadOnly();
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index)
		{
			ThrowReadOnly();
		}

		/// <summary>
		/// Gets or sets the <see cref="Xevle.Core.Collections.Generic.ReadOnlyList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public T this [int index]
		{
			get { return list[index]; }
			set { ThrowReadOnly(); }
		}
		#endregion

		#region Implements ICollection<T>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add(T item)
		{
			ThrowReadOnly();
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			ThrowReadOnly();
		}
			
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return list.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly
		{
			get { return true; }
		}
			
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove(T item)
		{
			ThrowReadOnly();
			return false; // to silence the compiler
		}
		#endregion

		#region IEnumerable/<T>
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
		#endregion

		#region IList
		/// <summary>
		/// Add the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		int IList.Add(object value)
		{
			ThrowReadOnly();
			return -1; // to silence the compiler
		}

		/// <summary>
		/// Contains the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		bool IList.Contains(object value)
		{
			if (value is T) return list.Contains((T)value);
			if (value == null && default(T) == null) return list.Contains((T)value);
			return false;
		}

		/// <summary>
		/// Indexs the of.
		/// </summary>
		/// <returns>The of.</returns>
		/// <param name="value">Value.</param>
		int IList.IndexOf(object value)
		{
			if (value is T) return list.IndexOf((T)value);
			if (value == null && default(T) == null) return list.IndexOf((T)value);
			return -1;
		}

		void IList.Insert(int index, object value)
		{
			ThrowReadOnly();
		}

		/// <summary>
		/// Gets a value indicating whether this instance is fixed size.
		/// </summary>
		/// <value><c>true</c> if this instance is fixed size; otherwise, <c>false</c>.</value>
		public bool IsFixedSize
		{
			get { return true; }
		}

		/// <summary>
		/// Remove the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		void IList.Remove(object value)
		{
			ThrowReadOnly();
		}

		/// <summary>
		/// Gets or sets the <see cref="Xevle.Core.Collections.Generic.ReadOnlyList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		object IList.this [int index]
		{
			get { return list[index]; }
			set { ThrowReadOnly(); }
		}
		#endregion

		#region ICollection
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="index">Index.</param>
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)list).CopyTo(array, index);
		}

		/// <summary>
		/// Gets a value indicating whether this instance is synchronized.
		/// </summary>
		/// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
		public bool IsSynchronized
		{
			get { return false; }
		}

		/// <summary>
		/// Gets the sync root.
		/// </summary>
		/// <value>The sync root.</value>
		public object SyncRoot
		{
			get { return ((ICollection)list).SyncRoot; }
		}
		#endregion
	}
}
