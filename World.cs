using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OpenRPG.Graphics;
using SSize = System.Drawing.Size;

namespace OpenRPG
{
	public class World
	{
		public IEnumerable<Actor> Actors { get { return actors; } }
		public int AbsoluteTicks;

		public readonly Game Game;
		public readonly Map Map;
		public readonly LineRenderer LineRenderer;

		public SSize WindowSize { get { return Game.WindowSize; } }
		public Rectangle WindowClientBounds { get { return Game.ClientBounds; } }

		readonly List<Actor> actors = new List<Actor>();
		readonly Queue<Action<World>> frameEndActions = new Queue<Action<World>>(100);

		public World(Game game)
		{
			Game = game;
			Map = new Map(new SSize(16, 16), new SSize(10, 10));
			LineRenderer = new LineRenderer(this);
		}

		public void AddFrameEndAction(Action<World> a)
		{
			frameEndActions.Enqueue(a);
		}

		public void AddActor(Actor a)
		{
			actors.Add(a);
		}

		public void Tick()
		{
			if (AbsoluteTicks == 0)
			{
				Console.WriteLine("Actor types from meta:");

				foreach (var actor in actors)
				{
					Console.WriteLine("\t" + actor.Info.Name);

					foreach (var trait in actor.Info.TraitInfos)
						Console.WriteLine("\t\t" + trait.GetType().ToString().ReverseSubstring(4));
				}
			}

			foreach (var actor in actors)
				foreach (var trait in actor.TraitsImplementing<ITick>())
					trait.Tick(actor);

			while (frameEndActions.Count != 0)
				frameEndActions.Dequeue()(this);

			AbsoluteTicks++;
		}

		public void TickRender()
		{
			LineRenderer.DrawRectangle
			(
				new Point2(0, 0),
				new Point2(WindowSize.Width, WindowSize.Height),
				Color.Red
			);

			foreach (var actor in actors)
				foreach (var trait in actor.TraitsImplementing<ITickRender>())
					trait.TickRender(actor);
		}

		public void RenderImage(Sprite sprite, Rectangle sourceRect, Point2 topLeft)
		{
			var screenRect = new Rectangle(topLeft.X, topLeft.Y, sourceRect.Width, sourceRect.Height);

			Game.SpriteBatch.Draw(sprite.Texture,
				screenRect,
				sourceRect,
				Color.White);
		}
	}
}
