using System;

namespace OpenRPG.Traits
{
	public class HealthInfo : ITraitInfo
	{
		public readonly int Value = 100;

		public object CreateTrait(Actor actor) { return new Health(actor, this); }
	}

	public class Health : ITrait
	{
		public int Value;

		public readonly int MaxValue;

		public Health(Actor self, HealthInfo info)
		{
			MaxValue = Value = info.Value;
		}
	}
}
