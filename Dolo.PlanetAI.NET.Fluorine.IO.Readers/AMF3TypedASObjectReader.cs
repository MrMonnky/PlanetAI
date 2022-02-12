using System;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3TypedASObjectReader : IReflectionOptimizer
{
	private string _typeIdentifier;

	public AMF3TypedASObjectReader(string typeIdentifier)
	{
		_typeIdentifier = typeIdentifier;
	}

	public object CreateInstance()
	{
		throw new NotImplementedException();
	}

	public object ReadData(AMFReader reader, ClassDefinition classDefinition)
	{
		ASObject aSObject = new ASObject(_typeIdentifier);
		reader.AddAMF3ObjectReference(aSObject);
		string text = reader.ReadAMF3String();
		aSObject.TypeName = _typeIdentifier;
		while (text != string.Empty)
		{
			object value = reader.ReadAMF3Data();
			aSObject.Add(text, value);
			text = reader.ReadAMF3String();
		}
		return aSObject;
	}
}
