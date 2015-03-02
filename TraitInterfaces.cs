using System;

namespace OpenRPG
{
	public interface ITraitInfo { object CreateTrait(Actor actor); }

	public interface ITick { void Tick(Actor self); }
	public interface ITickRender { void TickRender(Actor self); }
}
