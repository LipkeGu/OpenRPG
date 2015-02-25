using System;

namespace OpenRPG
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			using (var game = new Game())
				game.Run();

			return 0;
		}
	}
}