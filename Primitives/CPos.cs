using System;

namespace OpenRPG
{
	public struct CPos
	{
		public readonly int X, Y;

		public CPos(int x, int y, int z)
		{
			X = x;
			Y = y;
		}

		public static CPos Empty
		{
			get { return new CPos(0, 0, 0); }
		}

		public static bool operator ==(CPos me, CPos other)
		{
			return me.X == other.X && me.Y == other.Y;
		}

		public static bool operator !=(CPos me, CPos other)
		{
			return !(me == other);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CPos))
				return false;

			return this == (CPos)obj;
		}

		public override int GetHashCode()
		{
			return X ^ Y;
		}

		public override string ToString()
		{
			return "({0},{1})".F(X, Y);
		}
	}
}
