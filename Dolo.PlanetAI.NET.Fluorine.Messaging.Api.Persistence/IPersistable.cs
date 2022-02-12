using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

internal interface IPersistable
{
	bool IsPersistent { get; set; }

	string Name { get; set; }

	string Path { get; set; }

	long LastModified { get; }

	IPersistenceStore Store { get; set; }

	void Serialize(AMFWriter writer);

	void Deserialize(AMFReader reader);
}
