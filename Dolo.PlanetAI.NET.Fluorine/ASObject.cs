using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine;

[Serializable]
internal class ASObject : Dictionary<string, object>
{
	private string _typeName;

	public string TypeName
	{
		get
		{
			return _typeName;
		}
		set
		{
			_typeName = value;
		}
	}

	public bool IsTypedObject => _typeName != null && _typeName != string.Empty;

	public ASObject()
	{
	}

	public ASObject(string typeName)
	{
		_typeName = typeName;
	}

	public ASObject(IDictionary<string, object> dictionary)
		: base(dictionary)
	{
	}

	public ASObject(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
