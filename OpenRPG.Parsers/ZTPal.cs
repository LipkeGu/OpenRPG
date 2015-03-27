using System;
using System.IO;
using System.Drawing;

namespace OpenRPG.Parsers
{
	public class ZTPalette
	{
		readonly uint[] values = new uint[256];
		public int EntryCount { get { return values.Length; } }

		public static ZTPalette FromFile(string filename)
		{
			using (var s = File.OpenRead(filename))
				return new ZTPalette(s);
		}

		public ZTPalette(uint[] values)
		{
			var l = values.Length;
			if (l != 256)
			{
				Console.WriteLine("Created ZTPalette with bogus number of entries ({0}).", l);
				Environment.Exit(1);
			}
		}

		public void WriteToDisk(string filename)
		{
			var bytes = new byte[256 * 4];

			for (var i = 0; i < 256; i++)
			{
				var val = values[i];

				// Rip these values out of the uint
				var r = (byte)(val >> 24);
				var g = (byte)(val >> 16);
				var b = (byte)(val >>  8);
				var a = (byte)(val >>  0);

				bytes[i * 4 + 0] = r;
				bytes[i * 4 + 1] = g;
				bytes[i * 4 + 2] = b;
				bytes[i * 4 + 3] = a;
			}

			File.WriteAllBytes(filename, bytes);
		}

		public ZTPalette(Stream s)
		{
			for (var i = 0; i < 256; i++)
			{
				var r = s.ReadByte();
				var g = s.ReadByte();
				var b = s.ReadByte();
				var a = s.ReadByte();

				// Entry 0 is always FF 00 00 00
				if (i == 0 && (g > 0 || g > 0 || a > 0))
				{
					Console.WriteLine("Bogus ZTPal.");
					Environment.Exit(1);
				}

				// Shove these values into a uint (4 bytes, rgba)
				SetColor(i, r, g, b, a);
			}
		}

		public Color GetColor(int i)
		{
			return Color.FromArgb((int)values[i]);
		}

		public void SetColor(int i, int r, int g, int b, int a)
		{
			values[i] = (uint)
			(
				(r   << 24) |
				(g   << 16) |
				(b   <<  8) |
				(a   <<  0)
			);
		}

		public void SetColor(int i, Color c)
		{
			values[i] = (uint)c.ToArgb();
		}
	}
}