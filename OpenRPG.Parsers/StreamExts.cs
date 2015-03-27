using System;
using System.Drawing;
using System.IO;

namespace OpenRPG.Parsers
{
	public static class StreamExts
	{
		public static Color ReadRGBA(this Stream s)
		{
			var r = s.ReadByte();
			var g = s.ReadByte();
			var b = s.ReadByte();
			var a = s.ReadByte();

			return Color.FromArgb(a, r, g, b);
		}

		public static byte[] ReadBytes(this Stream s, int count)
		{
			var ret = new byte[count];

			s.ReadBytes(ret, 0, count);
			return ret;
		}

		public static void ReadBytes(this Stream s, byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int bytesRead;

				if ((bytesRead = s.Read(buffer, offset, count)) == 0)
					throw new EndOfStreamException();

				offset += bytesRead;
				count -= bytesRead;
			}
		}

		public static byte ReadUByte(this Stream s)
		{
			return ReadBytes(s, 1)[0];
		}
	}
}