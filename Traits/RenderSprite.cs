using System;
using Microsoft.Xna.Framework;
using OpenRPG.Graphics;
using SSize = System.Drawing.Size;

namespace OpenRPG.Traits
{
	public class RenderSpriteInfo : ITraitInfo
	{
		public readonly string Animation = null;

		public object CreateTrait(Actor actor) { return new RenderSprite(actor, this); }
	}

	public class RenderSprite : ITrait, ITickRender
	{
		public Sequence CurrentSequence { get; private set; }

		readonly World world;
		readonly string image;
		readonly Sequence[] sequences;
		readonly int ticksPerFrame;

		int currentFrame;
		int ticks;

		public RenderSprite(Actor self, RenderSpriteInfo info)
		{
			world = self.World;
			image = info.Animation ?? self.Info.Name;

			sequences = self.World.Game.Animations[image].Sequences;
			if (sequences.Length == 0)
				throw new MetadataException("Animation `{0}` has no sequences.".F(image));

			CurrentSequence = sequences[0];
			ticksPerFrame = CurrentSequence.TicksPerFrame;
			currentFrame = 0;
			ticks = ticksPerFrame;
		}

		public void TickRender(Actor self)
		{
			var sprite = CurrentSequence.Sprite;

			var frameSize = CurrentSequence.FrameSize;

			var row = Math.Floor((double)currentFrame / CurrentSequence.FramesPerRow);
			var col = Math.Floor((double)currentFrame % CurrentSequence.FramesPerRow);

			var sourceRect = new Rectangle(
				(int)(col * frameSize.Width),
				(int)(row * frameSize.Height),
				frameSize.Width,
				frameSize.Height);

			if (--ticks == 0)
			{
				currentFrame++;
				ticks = ticksPerFrame;
			}

			if (currentFrame > CurrentSequence.Length - 1)
				currentFrame = 0;

			// TODO: World -> Screen
			var pos = new Point2(self.WorldPosition.X, self.WorldPosition.Y);
			world.RenderImage(sprite, sourceRect, pos);

			_DrawSpriteBounds(pos, new SSize(frameSize.Width, frameSize.Height));
		}

		void _DrawSpriteBounds(Point2 topLeft, SSize size)
		{
			var bottomRight = new Point2(topLeft.X + size.Width, topLeft.Y + size.Height);
			world.LineRenderer.DrawRectangle(topLeft, bottomRight, Color.Blue);
		}
	}
}
