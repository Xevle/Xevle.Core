using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xevle.Core.Collections.Generic
{
	/// <summary>
	/// Priority queue.
	/// </summary>
	[DebuggerDisplay("Count = {Count}")]
	public class PriorityQueue<T>
	{
		List<T> entries;
		IComparer<T> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.PriorityQueue`1"/> class.
		/// </summary>
		public PriorityQueue()
		{
			entries = new List<T>();
			comparer = Comparer<T>.Default;
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return entries.Count; }
		}

		/// <summary>
		/// Empty this instance.
		/// </summary>
		public bool Empty()
		{
			return entries.Count == 0;
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			entries.Clear();
		}

		/// <summary>
		/// Add the specified entry.
		/// </summary>
		/// <param name="entry">Entry.</param>
		public void Add(T entry)
		{
			if (entry == null) throw new ArgumentNullException("entry");

			entries.Add(entry);
			if (entries.Count <= 1) return;

			int emptyslot = entries.Count - 1;
			for (int i = (emptyslot - 1) / 2; emptyslot > 0 && comparer.Compare(entries[i], entry) == 1; i = (emptyslot - 1) / 2)
			{
				entries[emptyslot] = entries[i];
				emptyslot = i;
			}

			entries[emptyslot] = entry;
		}

		/// <summary>
		/// Top this instance.
		/// </summary>
		public T Top()
		{
			if (entries.Count == 0) return default(T);
			return entries[0];
		}

		/// <summary>
		/// Pop this instance.
		/// </summary>
		public void Pop()
		{
			if (entries.Count > 1)
			{
				// save last slot's entry
				int lastslot = entries.Count - 1;
				T entry = entries[lastslot];

				// move top entry to last slot
				entries[lastslot] = entries[0];

				int emptyslot = 0;
				int i = 2 * emptyslot + 2;

				// move empty slot down the list
				for (; i < lastslot; i = 2 * i + 2)
				{
					if (comparer.Compare(entries[i], entries[i - 1]) == 1) i--;
					entries[emptyslot] = entries[i];
					emptyslot = i;
				}

				if (i == lastslot)
				{
					entries[emptyslot] = entries[lastslot - 1];
					emptyslot = lastslot - 1;
				}

				// re-add saved entry
				for (i = (emptyslot - 1) / 2; emptyslot > 0 && comparer.Compare(entries[i], entry) == 1; i = (emptyslot - 1) / 2)
				{
					entries[emptyslot] = entries[i];
					emptyslot = i;
				}

				entries[emptyslot] = entry;
			}

			entries.RemoveAt(entries.Count - 1);
		}
	}
}