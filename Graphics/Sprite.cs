using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRPG.Graphics
{
	public class Sprite
	{
		public readonly Texture2D Texture;
		public readonly int FrameCount;

		public int CurrentFrame { get; private set; }

		public Sprite(Game game, string imageName)
		{
			imageName = imageName.EndsWith(".png") ? imageName : imageName + ".png";

			using (var s = File.Open(game.Content.RootDirectory + "/" +	imageName, FileMode.Open))
				Texture = Texture2D.FromStream(game.GraphicsDevice, s);
		}

		public void RenderAt(Point topLeft)
		{
			// TODO
		}
	}
}
