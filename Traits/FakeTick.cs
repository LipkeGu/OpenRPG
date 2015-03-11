using System;

namespace OpenRPG.Traits
{
	public class FakeTickInfo : ITraitInfo
	{
		// Measured in ticks .. every 2 seconds
		public readonly int Interval = 60;

		public object CreateTrait(Actor actor) { return new FakeTick(actor, this); }
	}

	public class FakeTick : ITrait, ITick
	{
		int ticks;
		readonly FakeTickInfo info;

		public FakeTick(Actor self, FakeTickInfo info)
		{
			this.info = info;
			ticks = info.Interval;

			Console.WriteLine("Created FakeTick.");
		}

		public void Tick(Actor self)
		{
			if (--ticks > 0)
				return;

			ticks = info.Interval;
			Console.WriteLine("Fake tick!");
		}
	}
}
