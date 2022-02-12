using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal interface IReversible : IEnumerable
{
	IEnumerable Reversed { get; }
}
