using System;
using System.Drawing;

namespace OpenRPG
{
	public static class FieldLoader
	{
		public static T Load<T>(string str)
		{
			return (T)Load(str, typeof(T));
		}

		public static object Load(string str, Type fieldType)
		{
			str = str.Trim();

			if (fieldType == typeof(string))
				return str;

			if (fieldType == typeof(int))
				return int.Parse(str);

			if (fieldType == typeof(Size))
			{
				var parts = str.Split(',');
				if (parts.Length != 2)
					throw new MetadataException("Cannot parse `{0}` into Size; incorrect number of members."
						.F(str));

				var w = int.Parse(parts[0]);
				var h = int.Parse(parts[1]);

				return new Size(w, h);
			}

			return null;
		}
	}
}
