using System;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class PersistableAttributeStore : AttributeStore, IPersistable
{
	protected bool _persistent = true;

	protected string _name;

	protected string _path;

	protected string _type;

	protected long _lastModified = -1L;

	protected IPersistenceStore _store = null;

	public virtual string Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
		}
	}

	public virtual bool IsPersistent
	{
		get
		{
			return _persistent;
		}
		set
		{
			_persistent = value;
		}
	}

	public virtual string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	public virtual string Path
	{
		get
		{
			return _path;
		}
		set
		{
			_path = value;
		}
	}

	public virtual long LastModified => _lastModified;

	public virtual IPersistenceStore Store
	{
		get
		{
			return _store;
		}
		set
		{
			_store = value;
			if (_store != null)
			{
				_store.Load(this);
			}
		}
	}

	public PersistableAttributeStore(string type, string name, string path, bool persistent)
	{
		_name = name;
		_path = path;
		_type = type;
		_persistent = persistent;
	}

	public void Serialize(AMFWriter writer)
	{
	}

	public void Deserialize(AMFReader reader)
	{
	}

	protected void OnModified()
	{
		_lastModified = Environment.TickCount;
		if (_store != null)
		{
			_store.Save(this);
		}
	}

	public override bool RemoveAttribute(string name)
	{
		bool flag = base.RemoveAttribute(name);
		if (flag && !name.StartsWith("_transient"))
		{
			OnModified();
		}
		return flag;
	}

	public override void RemoveAttributes()
	{
		base.RemoveAttributes();
		OnModified();
	}

	public override bool SetAttribute(string name, object value)
	{
		bool flag = base.SetAttribute(name, value);
		if (flag && !name.StartsWith("_transient"))
		{
			OnModified();
		}
		return flag;
	}

	public override void SetAttributes(IAttributeStore values)
	{
		base.SetAttributes(values);
		OnModified();
	}

	public override void SetAttributes(IDictionary<string, object> values)
	{
		base.SetAttributes(values);
		OnModified();
	}
}
