namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3BooleanTrueReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return true;
	}
}
