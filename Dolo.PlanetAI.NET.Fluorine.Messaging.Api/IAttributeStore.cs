using System.Collections;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IAttributeStore
{
	bool IsEmpty { get; }

	object this[string name] { get; set; }

	int AttributesCount { get; }

	ICollection GetAttributeNames();

	bool SetAttribute(string name, object value);

	void SetAttributes(IDictionary<string, object> values);

	void SetAttributes(IAttributeStore values);

	object GetAttribute(string name);

	object GetAttribute(string name, object defaultValue);

	bool HasAttribute(string name);

	bool RemoveAttribute(string name);

	void RemoveAttributes();

	void CopyTo(object[] array, int index);
}
