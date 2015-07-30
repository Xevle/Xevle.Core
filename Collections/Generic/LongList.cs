using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xevle.Core.Collections.Generic
{
	/// <summary>
	/// Long list.
	/// </summary>
	[DebuggerDisplay("Count = {Count}")]
	public class LongList<T> : IEnumerable<T>
	{
		private List<List<T>> blocks;
		private static ulong maximumBlockLength = 1 << 27;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Generic.LongList`1"/> class.
		/// </summary>
		public LongList()
		{
			blocks = new List<List<T>>();
			ExtendBlocks();
		}

		/// <summary>
		/// Gets or sets the <see cref="Xevle.Core.Collections.Generic.LongList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public T this [ulong index]
		{
			get
			{
				int blockNumber = (int)(index / maximumBlockLength);
				int offset = (int)(index % maximumBlockLength);
				return blocks[blockNumber][offset];
			}
			set
			{
				int blockNumber = (int)(index / maximumBlockLength);
				int offset = (int)(index % maximumBlockLength);
				blocks[blockNumber][offset] = value;
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public ulong Count
		{
			get
			{
				ulong ret = 0;
				foreach (List<T> list in blocks) ret += (ulong)list.Count;
				return ret;
			}
		}

		/// <summary>
		/// Contains the specified val.
		/// </summary>
		/// <param name="val">Value.</param>
		public bool Contains(T val)
		{
			foreach (List<T> block in blocks)
			{
				if (block.Contains(val)) return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the last block.
		/// </summary>
		/// <value>The last block.</value>
		private List<T> LastBlock
		{
			get { return blocks[blocks.Count - 1]; }
		}

		private ulong FreeSpaceInLastBlock()
		{
			return maximumBlockLength - (ulong)LastBlock.Count;
		}

		private void ExtendBlocks()
		{
			blocks.Add(new List<T>());
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
			foreach (List<T> block in blocks)
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
			foreach (List<T> block in blocks)
			{
				foreach (T x in block)
				{
					yield return x;
				}
			}
		}
	}
}
