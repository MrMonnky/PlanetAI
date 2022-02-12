using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

internal interface IReflectionOptimizer
{
	object CreateInstance();

	object ReadData(AMFReader reader, ClassDefinition classDefinition);
}
