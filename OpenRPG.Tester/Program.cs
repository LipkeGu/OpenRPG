using System;
using OpenRPG.Parsers;

namespace OpenRPG.Tester
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			if (args.Length == 0)
				Environment.Exit(1);

			Console.WriteLine("OpenRPG.Tester -- used to test format parsing");
			Console.WriteLine("Currently only works for palettes.");
			Console.WriteLine();

			var file = args[0];
			var pal = ZTPalette.FromFile(file);

			Console.WriteLine("Read {0} entries.", pal.EntryCount);
		}
	}
}