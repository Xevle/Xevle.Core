using System.Collections.Generic;

namespace Xevle.Core.Collections.Generic
{
	/// <summary>
	/// Long hash set.
	/// </summary>
	public class LongHashSet<T> : IEnumerable<T>
	{
		private List<HashSet<T>> blocks;
		private static ulong maximumBlockLength = 1 << 26;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.LongHashSet`1"/> class.
		/// </summary>
		public LongHashSet()
		{
			blocks = new List<HashSet<T>>();
			ExtendBlocks();
		}

		/// <summary>
		/// Contains the specified val.
		/// </summary>
		/// <param name="val">Value.</param>
		public bool Contains(T val)
		{
			foreach (HashSet<T> block in blocks)
			{
				if (block.Contains(val)) return true;
			}

			return false;
		}

		private HashSet<T> LastBlock
		{
			get { return blocks[blocks.Count - 1]; }
		}

		private ulong FreeSpaceInLastBlock()
		{
			return maximumBlockLength - (ulong)LastBlock.Count;
		}

		private void ExtendBlocks()
		{
			blocks.Add(new HashSet<T>());
		}

		/// <summary>
		/// Add the specified x.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		public void Add(T x)
		{
			if (FreeSpaceInLastBlock() == 0) ExtendBlocks();
			LastBlock.Add(x);
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			blocks.Clear();
			ExtendBlocks();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (HashSet<T> block in blocks)
			{
				foreach (T x in block)
				{
					yield return x;
				}
			}
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			foreach (HashSet<T> block in blocks)
			{
				foreach (T x in block)
				{
					yield return x;
				}
			}
		}
	}
}
