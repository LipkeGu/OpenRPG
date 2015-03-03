using System;
using Microsoft.Xna.Framework;

namespace OpenRPG.Traits
{
	public class RenderSpriteInfo : ITraitInfo
	{
		public readonly string Image = null;

		public object CreateTrait(Actor actor) { return new RenderSprite(actor, this); }
	}

	public class RenderSprite : ITrait, ITickRender
	{
		readonly string image;

		public RenderSprite(Actor self, RenderSpriteInfo info)
		{
			image = info.Image ?? self.Info.Name;
		}

		public void TickRender(Actor self)
		{
			var world = self.World;
			var game = world.Game;
			var anim = game.Animations[image];
			var sprite = anim.Sequences[anim.CurrentSequence].Sprite;

			// TODO: World -> Screen
			var pos = new Point(self.Position.X, self.Position.Y);
			world.RenderImage(sprite, pos);
		}
	}
}
