using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class FactoryInstance
{
	private IFlexFactory _factory;

	private string _id;

	private Hashtable _properties;

	private string _scope;

	private string _source;

	private string _attributeId;

	public string Id => _id;

	public virtual string Scope
	{
		get
		{
			return _scope;
		}
		set
		{
			_scope = value;
		}
	}

	public virtual string Source
	{
		get
		{
			return _source;
		}
		set
		{
			_source = value;
		}
	}

	public string AttributeId
	{
		get
		{
			return _attributeId;
		}
		set
		{
			_attributeId = value;
		}
	}

	public Hashtable Properties => _properties;

	public FactoryInstance(IFlexFactory factory, string id, Hashtable properties)
	{
		_factory = factory;
		_id = id;
		_properties = properties;
	}

	public virtual Type GetInstanceClass()
	{
		return null;
	}

	public virtual object Lookup()
	{
		return _factory.Lookup(this);
	}

	public virtual void OnOperationComplete(object instance)
	{
	}
}
