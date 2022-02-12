namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal interface IExternalizable
{
	void ReadExternal(IDataInput input);

	void WriteExternal(IDataOutput output);
}
