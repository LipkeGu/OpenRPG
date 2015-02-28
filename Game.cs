using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRPG
{
	public class Game : Microsoft.Xna.Framework.Game
	{
		public static readonly char PathSeperator = Path.DirectorySeparatorChar;
		
		int ticks;
		SpriteBatch spriteBatch;

		readonly World world;

		public readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		public Game()
		{
			new GraphicsDeviceManager(this);
			IsMouseVisible = true;
			Content.RootDirectory = "Content";
			TargetElapsedTime = TimeSpan.FromSeconds(1f / 30f);

			world = new World(this);
		}

		protected override void Update(GameTime gameTime)
		{
			++ticks;
			world.Tick();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			world.TickRender();
			spriteBatch.End();
			base.Draw(gameTime);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// https://github.com/flibitijibibo/FNA/issues/295
			var pngs = Directory.EnumerateFiles(Content.RootDirectory, "*.png");
			foreach (var png in pngs)
			{
				var filename = png.Split(PathSeperator)[1].Split('.')[0];
				Textures.Add(filename, Content.Load<Texture2D>(filename));
			}
		}
	}
}
