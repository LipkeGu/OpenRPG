using System;

namespace OpenRPG.Traits
{
	public class FakeTick : ITrait, ITick
	{
		int ticks;

		public void Tick(Actor self)
		{
			self.World.AddFrameEndAction(w =>
				Console.WriteLine("FakeTick {0}", ticks++)
			);
		}
	}
}
