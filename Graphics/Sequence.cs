using System.Drawing;

namespace OpenRPG.Graphics
{
	public class Sequence
	{
		public readonly string Name;
		public readonly int Length;
		public readonly int Ticks;
		public readonly Sprite Sprite;
		public readonly Size FrameSize;
		public readonly int FramesPerRow;

		public Sequence(string name, int length, int ticks, Sprite sprite, Size frameSize)
		{
			Name = name;
			Length = length;
			Ticks = ticks;
			Sprite = sprite;
			FrameSize = frameSize;

			// Assuming no borders around the frames
			// and that the row spans the entire sheet
			// TODO: don't assume
			FramesPerRow = sprite.Size.Width / frameSize.Width;
		}
	}
}
