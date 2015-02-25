using System.Collections.Generic;
using OpenRPG.Traits;

namespace OpenRPG
{
	public class World
	{
		public readonly List<Actor> Actors = new List<Actor>();

		public World()
		{
			var tempActor = new Actor(this);
			var ft = new FakeTick();

			tempActor.AddTrait(ft);
			AddActor(tempActor);
		}

		public void AddActor(Actor a)
		{
			Actors.Add(a);
		}

		public void Tick()
		{
			foreach (var actor in Actors.ToArray())
				foreach (var trait in actor.TraitsImplementing<ITick>())
					trait.Tick(actor);
		}
	}
}
