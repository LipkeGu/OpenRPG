using System;
using System.Drawing;

namespace OpenRPG
{
	public struct Point2
	{
		public readonly int X, Y;

		public Point2(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Point2 Empty
		{
			get { return new Point2(0, 0); }
		}

		public static float Distance(Point2 me, Point2 other)
		{
			var dx = other.X - me.X;
			var dy = other.Y - me.Y;

			var xSq = Math.Pow(dx, 2);
			var ySq = Math.Pow(dy, 2);

			return (float)Math.Sqrt(xSq + ySq);
		}

		public static Point2 operator +(Point2 me, Size s)
		{
			return new Point2(me.X + s.Width, me.Y + s.Height);
		}

		public static Point2 operator -(Point2 me, Size s)
		{
			return new Point2(me.X - s.Width, me.Y - s.Height);
		}

		public static bool operator ==(Point2 me, Point2 other)
		{
			return (me.X == other.X && me.Y == other.Y);
		}

		public static bool operator !=(Point2 me, Point2 other)
		{
			return !(me == other);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Point2))
				return false;

			return (this == (Point2)obj);
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
