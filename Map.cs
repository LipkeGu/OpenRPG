using System;
using System.Drawing;

namespace OpenRPG
{
	public class Map
	{
		public const int WorldUnitsPerCell = 100;

		/// <summary>Number of cells on the X and Y axis.</summary>
		public readonly Size MapSize;

		public readonly Size CellSize;

		public Map(Size cellSize, Size mapSizeInCells)
		{
			CellSize = cellSize;
			MapSize = mapSizeInCells;
		}

		public CPos CellContaining(WPos p)
		{
			return new CPos
			(
			 	p.X / CellSize.Width,
				p.Y / CellSize.Height,
				p.Z / CellSize.Height
				//p.X / WorldUnitsPerCell,
				//p.Y / WorldUnitsPerCell,
				//p.Z / WorldUnitsPerCell
			);
		}
	}
}
