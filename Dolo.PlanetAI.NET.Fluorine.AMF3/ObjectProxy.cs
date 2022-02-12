using System.Collections;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal class ObjectProxy : Dictionary<string, object>
{
	public void ReadExternal(IDataInput input)
	{
		object obj = input.ReadObject();
		if (!(obj is IDictionary))
		{
			return;
		}
		IDictionary dictionary = obj as IDictionary;
		foreach (DictionaryEntry item in dictionary)
		{
			Add(item.Key as string, item.Value);
		}
	}

	public void WriteExternal(IDataOutput output)
	{
		ASObject aSObject = new ASObject();
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, object> current = enumerator.Current;
				aSObject.Add(current.Key, current.Value);
			}
		}
		output.WriteObject(aSObject);
	}
}
