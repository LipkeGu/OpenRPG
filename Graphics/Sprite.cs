using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRPG
{
	public class Sprite
	{
		public readonly string Filename;
		public readonly Size Size;
		public readonly Texture2D Texture;

		public Sprite(World world, string filename)
		{
			Filename = filename;

			using (var s = File.OpenRead(filename))
				Texture = Texture2D.FromStream(world.Game.GraphicsDevice, s);

			Size = new Size(Texture.Width, Texture.Height);
		}
	}
}
