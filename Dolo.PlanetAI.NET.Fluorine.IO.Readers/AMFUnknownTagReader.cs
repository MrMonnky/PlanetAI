using Dolo.PlanetAI.NET.Fluorine.Exceptions;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMFUnknownTagReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		throw new UnexpectedAMF();
	}
}
