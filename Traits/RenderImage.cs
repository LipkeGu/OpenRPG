using Microsoft.Xna.Framework;
using OpenRPG.Graphics;

namespace OpenRPG.Traits
{
	public class RenderImage : ITrait, ITickRender
	{
		public readonly Sprite Sprite;

		public RenderImage(Actor self, Sprite sprite)
		{
			Sprite = sprite;
		}

		public void TickRender(Actor self)
		{
			Sprite.RenderAt(Point.Zero);
		}
	}
}
