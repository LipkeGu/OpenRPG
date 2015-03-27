using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace OpenRPG
{
	public static class Exts
	{
		public static IEnumerable<string> GetNamespaces(this Assembly asm)
		{
			return asm.GetTypes().Select(t => t.Namespace).Distinct()
				.Where(n => n != null);
		}

		public static string F(this string str, params object[] fmt)
		{
			return string.Format(str, fmt);
		}

		public static string SubstringSkip(this string str, int start, int skipFromIndex, int skipToIndex)
		{
			var lhs = str.Substring(start, skipFromIndex - start);
			var rhs = str.Substring(skipToIndex);

			return lhs + rhs;
		}

		public static string ReverseSubstring(this string str, int count)
		{
			return str.Substring(0, str.Length - count);
		}

		public static Point TopLeft(this Rectangle rect)
		{
			return new Point(rect.Top, rect.Left);
		}

		public static Point TopRight(this Rectangle rect)
		{
			return new Point(rect.Top, rect.Right);
		}
	}
}
