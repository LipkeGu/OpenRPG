using System.Collections.Generic;
using System.Linq;

namespace OpenRPG
{
	public class Actor
	{
		bool isDead = false;
		public bool IsDead { get { return isDead; } }

		public readonly World World;

		readonly List<ITrait> traits = new List<ITrait>();

		// TODO:
		// * Possibly create Point3 type (x, y, z)
		// Should we use Point (with large ints) as Position and Vector3 as locomotion vectors?
		// If so: How will we translate from world -> screen if both use Point

		public Actor(World world)
		{
			World = world;
		}

		public void AddTrait(ITrait t)
		{
			traits.Add(t);
		}

		public T TraitOrNull<T>() where T : ITrait
		{
			return traits.OfType<T>().FirstOrDefault();
		}

		public IEnumerable<T> TraitsImplementing<T>() where T : class
		{
			return traits.Select(t => t as T).Where(t => t != null);
		}
	}

	public interface ITrait { }
}
