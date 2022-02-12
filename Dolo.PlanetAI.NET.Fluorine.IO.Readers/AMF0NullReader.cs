namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0NullReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return null;
	}
}
