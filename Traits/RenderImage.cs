using Microsoft.Xna.Framework;
using OpenRPG.Graphics;

namespace OpenRPG.Traits
{
	public class RenderImage : ITrait, ITickRender
	{
		public readonly SpriteSheet SpriteSheet;

		public RenderImage(Actor self, SpriteSheet ss)
		{
			SpriteSheet = ss;
		}

		public void TickRender(Actor self)
		{
			// TODO:
			// self.Position -> world -> screen
			// Sequence handler shouldn't be coupled to the Render* trait
			// Sequence data should contain possible frames for each sequence
			//     and current frame (how/where will we update this?)
			//     then tell the renderer (FNA's spritebatch?) to render the source rect px
			//     at the desired location on screen

			SpriteSheet.RenderAt(Point.Zero);
		}
	}
}
