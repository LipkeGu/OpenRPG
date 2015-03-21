using System;

namespace OpenRPG
{
	public struct WPos
	{
		public readonly int X, Y, Z;

		public WPos(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static WPos Empty
		{
			get { return new WPos(0, 0, 0); }
		}

		public static bool operator ==(WPos me, WPos other)
		{
			return me.X == other.X && me.Y == other.Y;
		}

		public static bool operator !=(WPos me, WPos other)
		{
			return !(me == other);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is WPos))
				return false;

			return this == (WPos)obj;
		}

		public override int GetHashCode()
		{
			return X ^ Y ^ Z;
		}

		public override string ToString()
		{
			return "{{0},{1},{2}}".F(X, Y, Z);
		}
	}
}
