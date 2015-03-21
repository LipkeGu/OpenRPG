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

			foreach (var traitNode in actorMeta.ChildTree.Nodes)
				TraitInfos.Add(LoadTraitInfo(traitNode));
		}

		public ITraitInfo LoadTraitInfo(MetadataNode traitNode)
		{
			var info = Game.CreateObject<ITraitInfo>(traitNode.Key + "Info");
			var fields = info.GetType().GetFields();

			foreach (var propertyNode in traitNode.ChildTree.Nodes)
			{
				// Trait properties should not have child nodes
				foreach (var bogusNode in propertyNode.ChildTree.Nodes)
					throw new Exception("Bogus node at {0}".F(bogusNode.Source));

				var field = fields.FirstOrDefault(f => f.Name == propertyNode.Key);
				if (field == null)
					continue;

				try
				{
					field.SetValue(info, FieldLoader.Load(propertyNode.ChildTree.Name, field.FieldType));
				}
				catch (Exception e)
				{
					throw new Exception("Bogus input value at {0}.\n".F(propertyNode.Source), e);
				}
			}

			return info;
		}

		public T GetOrNull<T>() where T : ITraitInfo
		{
			return TraitInfos.OfType<T>().FirstOrDefault();
		}
	}

	public class Actor
	{
		bool isDead = false;
		public bool IsDead { get { return isDead; } }

		// TODO: Cell position vs World position.
		public WPos WorldPosition { get; private set; }

		public readonly World World;
		public readonly string Name;
		public readonly ActorInfo Info;

		readonly List<ITrait> traits = new List<ITrait>();

		// TODO:
		// Should we use WPos (with large ints) as Position and therefor Vector3 as locomotion vectors?
		// If so: How will we translate from world -> screen if both use Point(3D -> 2D)

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
