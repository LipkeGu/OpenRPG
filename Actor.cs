using System.Collections.Generic;
using System.Linq;

namespace OpenRPG
{
	public class Actor
	{
		bool isDead = false;
		public bool IsDead { get { return isDead; } }

		public readonly World world;

		readonly List<Trait> traits = new List<Trait>();

		public Actor(World world)
		{
			this.world = world;
		}

		public void AddTrait(Trait t)
		{
			traits.Add(t);
		}

		public T TraitOrNull<T>() where T : Trait
		{
			return traits.OfType<T>().FirstOrDefault();
		}

		public IEnumerable<T> TraitsImplementing<T>() where T : class
		{
			return traits.Select(t => t as T).Where(t => t != null);
		}
	}

	public class Trait { }
}
