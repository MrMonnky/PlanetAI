using System;
using System.Collections;
using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class DotNetFactoryInstance : FactoryInstance
{
	private Type _cachedType;

	private object _applicationInstance;

	public override string Source
	{
		get
		{
			return base.Source;
		}
		set
		{
			if (base.Source != value)
			{
				base.Source = value;
				_cachedType = null;
			}
		}
	}

	public object ApplicationInstance
	{
		get
		{
			if (_applicationInstance == null)
			{
				lock (typeof(DotNetFactoryInstance))
				{
					if (_applicationInstance == null)
					{
						_applicationInstance = CreateInstance();
					}
				}
			}
			return _applicationInstance;
		}
	}

	public DotNetFactoryInstance(IFlexFactory flexFactory, string id, Hashtable properties)
		: base(flexFactory, id, properties)
	{
	}

	public object CreateInstance()
	{
		Type instanceClass = GetInstanceClass();
		object obj = null;
		if (instanceClass == null)
		{
			string @string = __Res.GetString("Type_InitError", Source);
			throw new MessageException(@string, new TypeLoadException(@string));
		}
		if (instanceClass.IsAbstract && instanceClass.IsSealed)
		{
			return instanceClass;
		}
		return Activator.CreateInstance(instanceClass, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[0], null);
	}

	public override Type GetInstanceClass()
	{
		if (_cachedType == null)
		{
			_cachedType = ObjectFactory.LocateInLac(Source);
		}
		return _cachedType;
	}
}
