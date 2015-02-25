using System;

namespace OpenRPG.Traits
{
	public interface ITick { void Tick(Actor self); }

	public class FakeTick : Trait, ITick
	{
		int ticks;

		public void Tick(Actor self)
		{
			Console.WriteLine(ticks++);
		}
	}
}
