using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal interface IModifiableCollection : ICollection, IEnumerable
{
	bool IsReadOnly { get; }

	bool Add(object key);

	bool AddIfNotContains(object key);

	int Remove(object key);

	void Clear();

	object Find(object key);

	ICollection FindAll(object key);
}
