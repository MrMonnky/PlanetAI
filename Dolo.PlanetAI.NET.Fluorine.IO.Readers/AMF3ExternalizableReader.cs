using System;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3ExternalizableReader : IReflectionOptimizer
{
	public object CreateInstance()
	{
		throw new NotImplementedException();
	}

	public object ReadData(AMFReader reader, ClassDefinition classDefinition)
	{
		object obj = ObjectFactory.CreateInstance(classDefinition.ClassName);
		if (obj == null)
		{
			string @string = __Res.GetString("Type_InitError", classDefinition.ClassName);
			throw new AMFException(@string);
		}
		reader.AddAMF3ObjectReference(obj);
		if (obj is IExternalizable)
		{
			IExternalizable externalizable = obj as IExternalizable;
			DataInput input = new DataInput(reader);
			externalizable.ReadExternal(input);
			return obj;
		}
		string string2 = __Res.GetString("Externalizable_CastFail", obj.GetType().FullName);
		throw new AMFException(string2);
	}
}
