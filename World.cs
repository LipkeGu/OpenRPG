using System;
using System.Collections.Generic;
using OpenRPG.Traits;
using OpenRPG.Graphics;

namespace OpenRPG
{
	public class World
	{
		public IEnumerable<Actor> Actors { get { return actors; } }
		public int AbsoluteTicks;

		public readonly Game Game;

		readonly List<Actor> actors = new List<Actor>();
		readonly Queue<Action<World>> frameEndActions = new Queue<Action<World>>(100);

		public World(Game game)
		{
			Game = game;
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
				Console.WriteLine("Actors from meta:");

				foreach (var actor in actors)
					Console.WriteLine("\t" + actor.Info.Name);
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
			foreach (var actor in actors)
				foreach (var trait in actor.TraitsImplementing<ITickRender>())
					trait.TickRender(actor);
		}
	}
}
