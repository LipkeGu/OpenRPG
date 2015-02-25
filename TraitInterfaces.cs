using System;

namespace OpenRPG
{
	public interface ITick { void Tick(Actor self); }
	public interface ITickRender { void TickRender(Actor self); }
}

