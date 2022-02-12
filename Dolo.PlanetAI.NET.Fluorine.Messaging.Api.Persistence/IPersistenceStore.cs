using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

internal interface IPersistenceStore
{
	bool Save(IPersistable obj);

	IPersistable Load(string name);

	bool Load(IPersistable obj);

	bool Remove(IPersistable obj);

	bool Remove(string name);

	ICollection GetObjectNames();

	ICollection GetObjects();
}
