using System;
using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class AttributeStore : DisposableBase, IAttributeStore
{
	protected Dictionary<string, object> _attributes = new Dictionary<string, object>();

	public bool IsEmpty
	{
		get
		{
			lock (((ICollection)_attributes).SyncRoot)
			{
				return _attributes.Count == 0;
			}
		}
	}

	public object this[string name]
	{
		get
		{
			return GetAttribute(name);
		}
		set
		{
			SetAttribute(name, value);
		}
	}

	public int AttributesCount
	{
		get
		{
			lock (((ICollection)_attributes).SyncRoot)
			{
				return _attributes.Count;
			}
		}
	}

	public virtual ICollection GetAttributeNames()
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			return _attributes.Keys;
		}
	}

	public virtual bool SetAttribute(string name, object value)
	{
		if (name == null)
		{
			return false;
		}
		lock (((ICollection)_attributes).SyncRoot)
		{
			object obj = null;
			if (_attributes.ContainsKey(name))
			{
				obj = _attributes[name];
			}
			_attributes[name] = value;
			return obj == null || value == obj || !value.Equals(obj);
		}
	}

	public virtual void SetAttributes(IDictionary<string, object> values)
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			foreach (KeyValuePair<string, object> value in values)
			{
				SetAttribute(value.Key, value.Value);
			}
		}
	}

	public virtual void SetAttributes(IAttributeStore values)
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			foreach (string attributeName in values.GetAttributeNames())
			{
				object attribute = values.GetAttribute(attributeName);
				SetAttribute(attributeName, attribute);
			}
		}
	}

	public virtual object GetAttribute(string name)
	{
		if (name == null)
		{
			return null;
		}
		lock (((ICollection)_attributes).SyncRoot)
		{
			if (_attributes.ContainsKey(name))
			{
				return _attributes[name];
			}
		}
		return null;
	}

	public virtual object GetAttribute(string name, object defaultValue)
	{
		if (name == null)
		{
			return null;
		}
		if (defaultValue == null)
		{
			throw new NullReferenceException("The default value may not be null.");
		}
		lock (((ICollection)_attributes).SyncRoot)
		{
			if (_attributes.ContainsKey(name))
			{
				return _attributes[name];
			}
			_attributes[name] = defaultValue;
			return null;
		}
	}

	public virtual bool HasAttribute(string name)
	{
		if (name == null)
		{
			return false;
		}
		lock (((ICollection)_attributes).SyncRoot)
		{
			return _attributes.ContainsKey(name);
		}
	}

	public virtual bool RemoveAttribute(string name)
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			if (HasAttribute(name))
			{
				_attributes.Remove(name);
				return true;
			}
			return false;
		}
	}

	public virtual void RemoveAttributes()
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			_attributes.Clear();
		}
	}

	public void CopyTo(object[] array, int index)
	{
		lock (((ICollection)_attributes).SyncRoot)
		{
			_attributes.Values.CopyTo(array, index);
		}
	}
}
