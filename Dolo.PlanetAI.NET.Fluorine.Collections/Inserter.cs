namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class Inserter : IOutputIterator
{
	private IModifiableCollection _collection;

	public Inserter(IModifiableCollection collection)
	{
		_collection = collection;
	}

	public void Add(object obj)
	{
		_collection.Add(obj);
	}
}
