using System;

namespace OpenRPG
{
	public class ActorInit
	{
		public readonly Actor Self;
		public World World { get { return Self.World; } }

		public ActorInit(Actor actor)
		{
			Self = actor;
		}
	}
}
