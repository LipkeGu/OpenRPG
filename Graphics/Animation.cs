using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenRPG.Graphics
{
	public class Animation
	{
		public readonly string Name;
		public readonly Sequence[] Sequences;

		public int CurrentSequence { get; private set; }

		public Animation(World world, string name, IEnumerable<MetadataNode> nodes)
		{
			Name = name;

			Sequences = new Sequence[nodes.Count()];

			var sprite = new Sprite(world, Game.ContentFileFromString(name + ".png"));
			Console.WriteLine("Creating Animation from `{0}` with {1} sequences.", name, Sequences.Length);

			var i = 0;
			foreach (var sequenceNode in nodes)
			{
				var properties = sequenceNode.ChildTree.ToDictionary();

				var length = 0;
				if (properties.ContainsKey("Length"))
					int.TryParse(properties["Length"].Value, out length);

				var size = new Size();
				if (properties.ContainsKey("FrameSize"))
					size = FieldLoader.Load<Size>(properties["FrameSize"].Value);

				// A default value I pulled out of thin air
				var ticksPerFrame = (int)(30 * 0.15);
				if (properties.ContainsKey("Ticks"))
					int.TryParse(properties["Ticks"].Value, out ticksPerFrame);

				// TODO: 1 sprite per anim, Sequence should point to N frames in that sprite
				Sequences[i] = new Sequence(sequenceNode.Key, length, ticksPerFrame, sprite, size);
			}
		}
	}
}
