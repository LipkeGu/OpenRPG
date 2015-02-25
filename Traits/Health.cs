namespace OpenRPG.Traits
{
	public class Health : ITrait
	{
		public int Value;

		public readonly int MaxValue;

		public Health(int max)
		{
			MaxValue = Value = max;
		}
	}
}
