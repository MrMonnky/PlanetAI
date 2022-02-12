using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal interface ISet : IModifiableCollection, ICollection, IEnumerable, IReversible
{
	IComparer Comparer { get; }
}
