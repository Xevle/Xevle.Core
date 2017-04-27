using System;
using System.Collections.Generic;

namespace Xevle.Core.Helper
{
	public static class CommandLineHelpers
	{
		public static Dictionary<string, string> GetCommandLine(string [] args)
		{
			Dictionary<string, string> ret = new Dictionary<string, string>();
			if (args.Length < 1) return ret; // no arguments given

			int fileCount = 0;
			string key;

			foreach (string value in args)
			{
				if (value [0] == '-')
				{
					int index = value.IndexOf(':');

					if (index >= 0)
					{
						key = value.Substring(1, index - 1);
						ret.Add(key, value.Substring(index + 1));
					}
					else
					{
						key = value.Substring(1);
						ret.Add(key, "true");
					}
				}
				else
				{
					key = String.Format("file{0}", fileCount++);
					ret.Add(key, value);
				}
			}

			return ret;
		}

		public static List<string> GetFilesFromCommandline(Dictionary<string, string> line)
		{
			List<string> ret = new List<string>();

			foreach (string key in line.Keys)
			{
				if (key.StartsWith("file", StringComparison.CurrentCulture))
				{
					ret.Add(line [key]);
				}
			}

			return ret;
		}
	}
}