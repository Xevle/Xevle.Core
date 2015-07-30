using System;

namespace Xevle.Core.Various
{
	/// <summary>
	/// ID generatotors.
	/// </summary>
	public static class IDGenerator
	{
		#region Static private variables
		/// <summary>
		/// Threadsafe counter
		/// </summary>
		static uint counter = 0;
		
		/// <summary>
		/// Random generator seed by system time.
		/// </summary>
		static Random randomOne = new Random();

		/// <summary>
		/// Random generator seed by TickCount.
		/// </summary>
		static Random randomTwo = new Random(Environment.TickCount);

		/// <summary>
		/// Random generator seed by working set.
		/// </summary>
		static Random randomThree = new Random((int)Environment.WorkingSet);
		#endregion

		#region Unique IDs
		/// <summary>
		/// Returns a GUID like "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"
		/// </summary>
		/// <returns>The GUI.</returns>
		public static string GetGUID()
		{
			// create parts of GUID
			string part1 = BitConverter.ToString(GetUniqueBytes(8));
			string part2 = BitConverter.ToString(GetUniqueBytes(4));
			string part3 = BitConverter.ToString(GetUniqueBytes(4));
			string part4 = BitConverter.ToString(GetUniqueBytes(4));
			string part5 = BitConverter.ToString(GetUniqueBytes(12));

			// return GUID
			return String.Format("{0}-{1}-{2}-{3}-{4}", part1, part2, part3, part4, part5);
		}

		public static byte[] GetUniqueBytes(int length)
		{
			// create byte[] array
			byte[] ret = new byte[length];
			randomOne.NextBytes(ret);
			if (length < 4) return ret; // special case if byte array smaller 4

			// normal case
			int lengthThree=length/3;

			byte[] randomOneBytes = new byte[lengthThree];
			byte[] randomTwoBytes = new byte[lengthThree];
			byte[] randomThreeBytes = new byte[lengthThree];

			// copy array
			Array.Copy(randomOneBytes, 0, ret, 0, randomOneBytes.Length);
			Array.Copy(randomTwoBytes, 0, ret, lengthThree, randomTwoBytes.Length);
			Array.Copy(randomThreeBytes, 0, ret, lengthThree*2, randomThreeBytes.Length);

			// return array
			return ret;
		}

		public static UInt64 GetUniqueUInt64()
		{
			// create random bytes
			byte[] randomOneBytes = new byte[4];
			byte[] randomTwoBytes = new byte[2];
			byte[] randomThreeBytes = new byte[2];

			randomOne.NextBytes(randomOneBytes);
			randomTwo.NextBytes(randomTwoBytes);
			randomThree.NextBytes(randomThreeBytes);
			
			// Create Int64 array
			byte[] Int64AsByteArray = new byte[8];

			Int64AsByteArray[0] = randomOneBytes[0];
			Int64AsByteArray[1] = randomOneBytes[1];
			Int64AsByteArray[2] = randomOneBytes[2];
			Int64AsByteArray[3] = randomOneBytes[3];
			Int64AsByteArray[4] = randomTwoBytes[0];
			Int64AsByteArray[5] = randomTwoBytes[1];
			Int64AsByteArray[6] = randomThreeBytes[0];
			Int64AsByteArray[7] = randomThreeBytes[1];

			// return integer
			return BitConverter.ToUInt64(Int64AsByteArray, 0);
		}

		public static string GetUniqueID()
		{
			if (counter > 4294967200) counter = 0;
			counter++;

			DateTime now = DateTime.Now;

			return now.Ticks.ToString().PadLeft(20, '0')
			+ "-"
			+ counter.ToString().PadLeft(10, '0')
			+ "-"
			+ randomOne.Next(999999999).ToString().PadLeft(9, '0')
			+ "-"
			+ randomTwo.Next(999999999).ToString().PadLeft(9, '0')
			+"-"
			+ randomThree.Next(999999999).ToString().PadLeft(9, '0');
		}
		#endregion

		#region Time IDs
		public static string GetTimeID()
		{
			return DateTime.Now.Ticks.ToString().PadLeft(20, '0');
		}

		public static string GetReadableTimeID()
		{
			DateTime now = DateTime.Now;
			return String.Format("[{0:D4}.{1:D2}.{2:D2}] -> [{3:D2}:{4:D2}:{5:D2}:{6:D3}]", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
		}
		#endregion
	}
}