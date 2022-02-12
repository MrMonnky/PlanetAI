namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal interface IAMFWriter
{
	bool IsPrimitive { get; }

	void WriteData(AMFWriter writer, object data);
}
