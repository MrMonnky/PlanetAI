using System;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0TypedASObjectReader : IReflectionOptimizer
{
	private string _typeIdentifier;

	public AMF0TypedASObjectReader(string typeIdentifier)
	{
		_typeIdentifier = typeIdentifier;
	}

	public object CreateInstance()
	{
		throw new NotImplementedException();
	}

	public object ReadData(AMFReader reader, ClassDefinition classDefinition)
	{
		ASObject aSObject = reader.ReadASObject();
		aSObject.TypeName = _typeIdentifier;
		return aSObject;
	}
}
