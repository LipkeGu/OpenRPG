namespace OpenRPG.Traits
{
	public class Health : Trait
	{
		public int Value;

		public readonly int MaxValue;

		public Health(int max)
		{
			MaxValue = Value = max;
		}
	}
}
