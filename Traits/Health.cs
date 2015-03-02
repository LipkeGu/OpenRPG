using System;

namespace OpenRPG.Traits
{
	public class HealthInfo : ITraitInfo
	{
		public readonly int Value = 100;

		public object CreateTrait(ActorInit init) { return new Health(init.Self, this); }
	}

	public class Health : ITrait
	{
		public int Value;

		public readonly int MaxValue;

		public Health(Actor self, HealthInfo info)
		{
			MaxValue = Value = info.Value;

			Console.WriteLine(Value);
		}
	}
}
