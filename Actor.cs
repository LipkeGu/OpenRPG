using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRPG
{
	public class ActorInfo
	{
		public readonly string Name;
		public readonly List<ITraitInfo> TraitInfos = new List<ITraitInfo>();

		public ActorInfo(string actorName, MetadataNode actorMeta)
		{
			Name = actorName;

			foreach (var traitNode in actorMeta.Tree.Nodes)
				TraitInfos.Add(LoadTraitInfo(traitNode));
		}

		public ITraitInfo LoadTraitInfo(MetadataNode traitNode)
		{
			var info = Game.CreateObject<ITraitInfo>(traitNode.Key + "Info");
			var fields = info.GetType().GetFields();

			foreach (var propertyNode in traitNode.Tree.Nodes)
			{
				// Trait properties should not have child nodes
				foreach (var bogusNode in propertyNode.Tree.Nodes)
					throw new Exception("Bogus node at {0}".F(bogusNode.Source));

				var field = fields.FirstOrDefault(f => f.Name == propertyNode.Key);
				if (field == null)
					continue;

				try
				{
					field.SetValue(info, ParseField(propertyNode.Tree.Value, field.FieldType));
				}
				catch (Exception e)
				{
					throw new Exception("Bogus input value at {0}.\n".F(propertyNode.Source), e);
				}
			}

			return info;
		}

		// TODO: Move this to a (static?) class so it doesn't clutter ActorInfo
		/// <summary>Used to set field values for constructed objects.</summary>
		static object ParseField(string str, Type fieldType)
		{
			if (fieldType == typeof(string))
				return str.Trim();

			if (fieldType == typeof(int))
				return int.Parse(str);

			return null;
		}
	}

	public class Actor
	{
		bool isDead = false;
		public bool IsDead { get { return isDead; } }

		public readonly World World;
		public readonly string Name;
		public readonly ActorInfo Info;

		readonly List<ITrait> traits = new List<ITrait>();

		// TODO:
		// * Possibly create Point3 type (x, y, z)
		// Should we use Point (with large ints) as Position and Vector3 as locomotion vectors?
		// If so: How will we translate from world -> screen if both use Point

		public Actor(World world, string name)
		{
			World = world;
			Name = name;
			Info = World.Game.ActorRules[name];
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
