using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRPG
{
	public class Game : Microsoft.Xna.Framework.Game
	{
		int ticks;
		SpriteBatch spriteBatch;

		readonly GraphicsDeviceManager graphics;
		readonly World world;

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			IsMouseVisible = true;
			Content.RootDirectory = "Content";
			TargetElapsedTime = TimeSpan.FromSeconds(1f / 30f);

			world = new World();
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
			base.Draw(gameTime);
			spriteBatch.End();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}
	}
}
