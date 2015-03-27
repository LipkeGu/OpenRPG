using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OpenRPG.Graphics
{
	public class LineRenderer
	{
		readonly SpriteBatch sb;
		readonly Texture2D pixel;

		public LineRenderer(World world)
		{
			sb = world.Game.SpriteBatch;

			pixel = new Texture2D(world.Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}

		public void DrawRectangle(Point topLeft, Point bottomRight, Color color)
		{
			var topRight = new Point(bottomRight.X, topLeft.Y);
			var bottomLeft = new Point(topLeft.X, bottomRight.Y);

			DrawLine(topLeft, topRight, color);
			DrawLine(topRight, bottomRight, color);
			DrawLine(bottomRight, bottomLeft, color);
			DrawLine(bottomLeft, topLeft, color);
		}

		/// <summary>Draws a line from one point to another in screen coordinates.</summary>
		public void DrawLine(Point start, Point end, Color color = default(Color), float thickness = 1f)
		{
			var distance = Distance(start, end);
			var angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
			DrawLine(start, distance, angle, color, thickness);
		}

		static float Distance(Point me, Point other)
		{
			var dx = other.X - me.X;
			var dy = other.Y - me.Y;

			var xSq = Math.Pow(dx, 2);
			var ySq = Math.Pow(dy, 2);

			return (float)Math.Sqrt(xSq + ySq);
		}

		/// <summary>Draws a line from a point out to a length in screen coordinates.</summary>
		public void DrawLine(Point location, float length, float angle, Color color, float thickness)
		{
			sb.Draw
			(
				pixel,
				new Vector2(location.X, location.Y),
				null,
				color,
				angle,
				Vector2.Zero,
				new Vector2(length, thickness),
				SpriteEffects.None,
				0
			);
		}
	}
}