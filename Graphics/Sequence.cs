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

		public Sequence(string name, int length, int ticks, Sprite sprite, Size frameSize)
		{
			Name = name;
			Length = length;
			Ticks = ticks;
			Sprite = sprite;
			FrameSize = frameSize;
		}
	}
}
