using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class DotNetFactory : IFlexFactory
{
	public FactoryInstance CreateFactoryInstance(string id, Hashtable properties)
	{
		DotNetFactoryInstance dotNetFactoryInstance = new DotNetFactoryInstance(this, id, properties);
		dotNetFactoryInstance.Source = properties["source"] as string;
		dotNetFactoryInstance.Scope = properties["scope"] as string;
		if (dotNetFactoryInstance.Scope == null)
		{
			dotNetFactoryInstance.Scope = "request";
		}
		dotNetFactoryInstance.AttributeId = properties["attribute-id"] as string;
		return dotNetFactoryInstance;
	}

	public object Lookup(FactoryInstance factoryInstance)
	{
		DotNetFactoryInstance dotNetFactoryInstance = factoryInstance as DotNetFactoryInstance;
		if (dotNetFactoryInstance.Scope == "application")
		{
			return dotNetFactoryInstance.ApplicationInstance;
		}
		return dotNetFactoryInstance.CreateInstance();
	}
}
