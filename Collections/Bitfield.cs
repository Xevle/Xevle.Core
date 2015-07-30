using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Xevle.Core.Collections
{
	/// <summary>
	/// Bitfield.
	/// </summary>
	public class Bitfield : ICollection, IEnumerable, IEnumerable<bool>
	{
		uint length;
		uint[] bitfield;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Bitfield"/> class.
		/// </summary>
		/// <param name="bits">Bits.</param>
		public Bitfield(Bitfield bits)
		{
			if (bits == null) throw new ArgumentNullException("bits");
			this.length = bits.length;
			bitfield = new uint[bits.bitfield.Length];
			Array.Copy(bits.bitfield, bitfield, bitfield.Length);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Bitfield"/> class.
		/// </summary>
		/// <param name="length">Length.</param>
		/// <param name="defaultValue">If set to <c>true</c> default value.</param>
		public Bitfield(uint length, bool defaultValue = false)
		{
			this.length = length;
			int uints = (int)(length + 31) / 32;
			bitfield = new uint[uints];

			if (defaultValue)
			{
				for (int i = 0; i < uints; i++) bitfield[i] = uint.MaxValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Bitfield"/> class.
		/// </summary>
		/// <param name="values">Values.</param>
		public Bitfield(List<bool> values)
		{
			if (values == null) throw new ArgumentNullException("values");
			length = (uint)values.Count;
			int uints = (int)(length + 31) / 32;
			bitfield = new uint[uints];

			for (int i = 0; i < length; i++) if (values[i]) Set(i);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Bitfield"/> class.
		/// </summary>
		/// <param name="values">Values.</param>
		public Bitfield(bool[] values)
		{
			if (values == null) throw new ArgumentNullException("values");
			length = (uint)values.Length;
			int uints = (int)(length + 31) / 32;
			bitfield = new uint[uints];

			for (int i = 0; i < length; i++) if (values[i]) Set(i);
		}

		/// <summary>
		/// Get the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public bool Get(int bit)
		{
			if (bit < 0 || bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be equal or greater than 0 and less than Length.");
			return (bitfield[bit / 32] & (1u << (bit % 32))) != 0;
		}

		/// <summary>
		/// Get the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public bool Get(uint bit)
		{
			if (bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be less than Length.");
			return (bitfield[(int)(bit / 32)] & (1u << ((int)(bit % 32)))) != 0;
		}

		/// <summary>
		/// Set the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public void Set(int bit)
		{
			if (bit < 0 || bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be equal or greater than 0 and less than Length.");
			bitfield[bit / 32] |= 1u << (bit % 32);
		}

		/// <summary>
		/// Set the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public void Set(uint bit)
		{
			if (bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be less than Length.");
			bitfield[(int)(bit / 32)] |= 1u << ((int)(bit % 32));
		}

		/// <summary>
		/// Set the specified from and to.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public void Set(int from, int to)
		{
			if (from < 0 || from >= length) throw new ArgumentOutOfRangeException("from", "Must be equal or greater than 0 and less than Length.");
			if (to < 0 || to >= length) throw new ArgumentOutOfRangeException("to", "Must be equal or greater than 0 and less than Length.");
			Set((uint)from, (uint)to);
		}

		/// <summary>
		/// Set the specified from and to.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public void Set(uint from, uint to)
		{
			if (from >= length) throw new ArgumentOutOfRangeException("from", "Must be less than Length.");
			if (to >= length) throw new ArgumentOutOfRangeException("to", "Must be less than Length.");

			if (from == to)
			{
				Set(from);
				return;
			}

			int uintPos = (int)(from / 32);
			int bit = (int)(from % 32);
			uint count = to - from + 1;

			if (bit != 0)
			{
				uint update = 0;
				for (int i = bit; i < 32 && count > 0; i++, count--) update |= 1u << i;
				bitfield[uintPos] |= update;
				uintPos++;
				bit = 0;
			}

			while (count >= 32)
			{
				bitfield[uintPos++] = uint.MaxValue;
				count -= 32;
			}

			if (count != 0)
			{
				uint update = 0;
				for (int i = 0; i < 32 && count > 0; i++, count--) update |= 1u << i;
				bitfield[uintPos] |= update;
			}
		}

		/// <summary>
		/// Sets all.
		/// </summary>
		public void SetAll()
		{
			for (int i = bitfield.Length - 1; i >= 0; i--) bitfield[i] = uint.MaxValue;
		}

		/// <summary>
		/// Reset the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public void Reset(int bit)
		{
			if (bit < 0 || bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be equal or greater than 0 and less than Length.");
			bitfield[bit / 32] &= ~(1u << (bit % 32));
		}

		/// <summary>
		/// Reset the specified bit.
		/// </summary>
		/// <param name="bit">Bit.</param>
		public void Reset(uint bit)
		{
			if (bit >= length) throw new ArgumentOutOfRangeException("bit", "Must be less than Length.");
			bitfield[(int)(bit / 32)] &= ~(1u << ((int)(bit % 32)));
		}

		/// <summary>
		/// Reset the specified from and to.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public void Reset(int from, int to)
		{
			if (from < 0 || from >= length) throw new ArgumentOutOfRangeException("from", "Must be equal or greater than 0 and less than Length.");
			if (to < 0 || to >= length) throw new ArgumentOutOfRangeException("to", "Must be equal or greater than 0 and less than Length.");
			Reset((uint)from, (uint)to);
		}

		/// <summary>
		/// Reset the specified from and to.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public void Reset(uint from, uint to)
		{
			if (from >= length) throw new ArgumentOutOfRangeException("from", "Must be less than Length.");
			if (to >= length) throw new ArgumentOutOfRangeException("to", "Must be less than Length.");

			if (from == to)
			{
				Reset(from);
				return;
			}

			int uintPos = (int)(from / 32);
			int bit = (int)(from % 32);
			uint count = to - from + 1;

			if (bit != 0)
			{
				uint update = 0;
				for (int i = bit; i < 32 && count > 0; i++, count--) update |= 1u << i;
				bitfield[uintPos] &= ~update;
				uintPos++;
				bit = 0;
			}

			while (count >= 32)
			{
				bitfield[uintPos++] = 0;
				count -= 32;
			}

			if (count != 0)
			{
				uint update = 0;
				for (int i = 0; i < 32 && count > 0; i++, count--) update |= 1u << i;
				bitfield[uintPos] &= ~update;
			}
		}

		/// <summary>
		/// Resets all.
		/// </summary>
		public void ResetAll()
		{
			for (int i = bitfield.Length - 1; i >= 0; i--) bitfield[i] = 0;
		}

		/// <summary>
		/// Gets or sets the <see cref="Xevle.Core.Collections.Bitfield"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public bool this [int index]
		{
			get { return Get(index); }
			set
			{
				if (value) Set(index);
				else Reset(index);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Xevle.Core.Collections.Bitfield"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public bool this [uint index]
		{
			get { return Get(index); }
			set
			{
				if (value) Set(index);
				else Reset(index);
			}
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="destination">Destination.</param>
		/// <param name="index">Index.</param>
		public void CopyTo(Array destination, int index)
		{
			if (destination == null) throw new ArgumentNullException("destination");
			if (index < 0) throw new ArgumentOutOfRangeException("index", "Must be not negative.");
			if (destination.Rank != 1) throw new ArgumentException("Must be a 1-dimensional array", "destination");

			bool[] dst = destination as bool[];
			if (dst == null) throw new NotSupportedException("Only bool[] supported.");

			if ((long)dst.Length - index < length) throw new ArgumentException("Array destination (starting at index) is not long enough.");

			for (int i = 0; i < length; i++) dst[index + i] = (bitfield[i / 32] & (1u << (i % 32))) != 0;
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return (int)length; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is synchronized.
		/// </summary>
		/// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
		public bool IsSynchronized
		{
			get { return false; }
		}

		object syncRoot;

		/// <summary>
		/// Gets the sync root.
		/// </summary>
		/// <value>The sync root.</value>
		public object SyncRoot
		{
			get
			{
				if (syncRoot == null) Interlocked.CompareExchange(ref syncRoot, new object(), null);
				return syncRoot;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BitfieldEnumerator(this);
		}

		IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
		{
			return new BitfieldEnumerator(this);
		}

		/// <summary>
		/// Gets the set bits.
		/// </summary>
		/// <value>The set bits.</value>
		public BitCollection SetBits
		{
			get { return new BitCollection(this, true); }
		}

		/// <summary>
		/// Gets the not set bits.
		/// </summary>
		/// <value>The not set bits.</value>
		public BitCollection NotSetBits
		{
			get { return new BitCollection(this, false); }
		}

		class BitfieldEnumerator : IEnumerator, IEnumerator<bool>
		{
			Bitfield bits;
			bool cur;
			long index;

			public BitfieldEnumerator(Bitfield bits)
			{
				this.bits = bits;
				index = -1;
			}

			public virtual bool MoveNext()
			{
				if (index < bits.length - 1)
				{
					index++;
					cur = bits.Get((uint)index);
					return true;
				}

				index = bits.Count;
				return false;
			}

			public void Reset()
			{
				index = -1;
			}

			object IEnumerator.Current
			{
				get
				{
					if (index == -1) throw new InvalidOperationException("Enumeration not started yet.");
					if (index >= bits.Count) throw new InvalidOperationException("Enumeration has ended");
					return cur;
				}
			}

			bool IEnumerator<bool>.Current
			{
				get
				{
					if (index == -1) throw new InvalidOperationException("Enumeration not started yet.");
					if (index >= bits.Count) throw new InvalidOperationException("Enumeration has ended");
					return cur;
				}
			}

			public void Dispose()
			{
			}
		}

		/// <summary>
		/// Bit collection.
		/// </summary>
		public class BitCollection : IEnumerable<uint>
		{
			Bitfield bits;
			bool value;

			/// <summary>
			/// Initializes a new instance of the <see cref="Xevle.Core.Collections.Bitfield+BitCollection"/> class.
			/// </summary>
			/// <param name="bits">Bits.</param>
			/// <param name="value">If set to <c>true</c> value.</param>
			public BitCollection(Bitfield bits, bool value)
			{
				this.bits = bits;
				this.value = value;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new BitfieldSetEnumerator(bits, value);
			}

			IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
			{
				return new BitfieldSetEnumerator(bits, value);
			}
		}

		class BitfieldSetEnumerator : IEnumerator<uint>
		{
			Bitfield bits;
			long index;
			bool value = true;

			public BitfieldSetEnumerator(Bitfield bits, bool value = true)
			{
				this.bits = bits;
				index = -1;
				this.value = value;
			}

			public virtual bool MoveNext()
			{
				while (index < bits.length - 1)
				{
					index++;
					if (bits.Get((uint)index) == value) return true;
				}

				index = bits.Count;
				return false;
			}

			public void Reset()
			{
				index = -1;
			}

			public virtual uint Current
			{
				get
				{
					if (index == -1) throw new InvalidOperationException("Enumeration not started yet.");
					if (index >= bits.Count) throw new InvalidOperationException("Enumeration has ended");
					return (uint)index;
				}
			}

			public void Dispose()
			{
			}

			object IEnumerator.Current
			{
				get
				{
					if (index == -1) throw new InvalidOperationException("Enumeration not started yet.");
					if (index >= bits.Count) throw new InvalidOperationException("Enumeration has ended");
					return (uint)index;
				}
			}
		}
	}
}
