using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal interface ISortedSet : ICollection, IEnumerable, IList
{
	ISortedSet TailSet(object limit);
}
