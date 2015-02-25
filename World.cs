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

		public readonly Game GameReference;

		readonly List<Actor> actors = new List<Actor>();
		readonly Queue<Action<World>> frameEndActions = new Queue<Action<World>>(100);

		public World(Game game)
		{
			GameReference = game;

			var tempActor = new Actor(this);
			var ft = new FakeTick();
			var sprite = new Sprite(game, "hero");
			var ri = new RenderImage(tempActor, sprite);

			tempActor.AddTrait(ft);
			tempActor.AddTrait(ri);
			AddActor(tempActor);
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
			Console.WriteLine("World tick {0}", AbsoluteTicks);

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
