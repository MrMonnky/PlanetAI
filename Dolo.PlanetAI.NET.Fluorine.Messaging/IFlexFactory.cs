using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal interface IFlexFactory
{
	FactoryInstance CreateFactoryInstance(string id, Hashtable properties);

	object Lookup(FactoryInstance factoryInstance);
}
