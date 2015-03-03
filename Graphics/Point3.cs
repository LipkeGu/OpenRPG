using System;
using System.Drawing;

namespace OpenRPG
{
	public struct Point3
	{
		public readonly int X, Y, Z;

		public Point3(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Point3 Empty
		{
			get { return new Point3(0, 0, 0); }
		}

		public static Point3 operator +(Point3 me, Size s)
		{
			return new Point3(me.X + s.Width, me.Y + s.Height, me.Z);
		}

		public static Point3 operator -(Point3 me, Size s)
		{
			return new Point3(me.X - s.Width, me.Y - s.Height, me.Z);
		}

		public static bool operator ==(Point3 me, Point3 other)
		{
			return (me.X == other.X && me.Y == other.Y);
		}

		public static bool operator !=(Point3 me, Point3 other)
		{
			return !(me == other);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Point3))
				return false;

			return (this == (Point3)obj);
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
