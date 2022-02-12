using System;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3TempObjectReader : IReflectionOptimizer
{
	public object CreateInstance()
	{
		throw new NotImplementedException();
	}

	public object ReadData(AMFReader reader, ClassDefinition classDefinition)
	{
		return reader.ReadAMF3Object(classDefinition);
	}
}
