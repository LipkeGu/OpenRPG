using System;
using Microsoft.Xna.Framework;
using OpenRPG.Graphics;

namespace OpenRPG.Traits
{
	public class RenderSpriteInfo : ITraitInfo
	{
		public readonly string Image = null;

		public object CreateTrait(Actor actor) { return new RenderSprite(actor, this); }
	}

	public class RenderSprite : ITrait, ITickRender
	{
		public Sequence CurrentSequence { get; private set; }

		readonly string image;
		readonly Sequence[] sequences;
		readonly int ticksPerFrame;

		int currentFrame;
		int ticks;

		public RenderSprite(Actor self, RenderSpriteInfo info)
		{
			image = info.Image ?? self.Info.Name;

			sequences = self.World.Game.Animations[image].Sequences;
			if (sequences.Length == 0)
				throw new MetadataException("Animation `{0}` has no sequences.".F(image));

			CurrentSequence = sequences[0];
			ticksPerFrame = CurrentSequence.Ticks;
			currentFrame = 0;
			ticks = ticksPerFrame;
		}

		public void TickRender(Actor self)
		{
			var world = self.World;
			var sprite = CurrentSequence.Sprite;

			var frameSize = CurrentSequence.FrameSize;

			// Y is hardcoded to 0 because I can't figure out how to calculate rows/columns correctly (using modulo)
			var sourceRect = new Rectangle(currentFrame * frameSize.Width, 0 * frameSize.Height, frameSize.Width, frameSize.Height);

			if (--ticks == 0)
			{
				currentFrame++;
				ticks = ticksPerFrame;
			}

			if (currentFrame > CurrentSequence.Length - 1)
				currentFrame = 0;

			// TODO: World -> Screen
			var pos = new Point(self.Position.X, self.Position.Y);
			world.RenderImage(sprite, sourceRect, pos);
		}
	}
}
