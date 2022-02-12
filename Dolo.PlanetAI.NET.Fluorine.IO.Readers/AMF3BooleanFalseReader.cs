namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3BooleanFalseReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return false;
	}
}
