using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class ClassMappingCollection : CollectionBase<ClassMapping>
{
	private Dictionary<string, string> _typeToCustomClass = new Dictionary<string, string>();

	private Dictionary<string, string> _customClassToType = new Dictionary<string, string>();

	public ClassMappingCollection()
	{
		Add("Dolo.PlanetAI.NET.Fluorine.AMF3.ArrayCollection", "flex.messaging.io.ArrayCollection");
		Add("Dolo.PlanetAI.NET.Fluorine.AMF3.ByteArray", "flex.messaging.io.ByteArray");
		Add("Dolo.PlanetAI.NET.Fluorine.AMF3.ObjectProxy", "flex.messaging.io.ObjectProxy");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.CommandMessage", "flex.messaging.messages.CommandMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.RemotingMessage", "flex.messaging.messages.RemotingMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.AsyncMessage", "flex.messaging.messages.AsyncMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.AcknowledgeMessage", "flex.messaging.messages.AcknowledgeMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.Messages.DataMessage", "flex.data.messages.DataMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.Messages.PagedMessage", "flex.data.messages.PagedMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.Messages.UpdateCollectionMessage", "flex.data.messages.UpdateCollectionMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.Messages.SequencedMessage", "flex.data.messages.SequencedMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.Messages.DataErrorMessage", "flex.data.messages.DataErrorMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.ErrorMessage", "flex.messaging.messages.ErrorMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.RemotingMessage", "flex.messaging.messages.RemotingMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Messages.RPCMessage", "flex.messaging.messages.RPCMessage");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.UpdateCollectionRange", "flex.data.UpdateCollectionRange");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Services.RemotingService", "flex.messaging.services.RemotingService");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Services.MessageService", "flex.messaging.services.MessageService");
		Add("Dolo.PlanetAI.NET.Fluorine.Data.DataService", "flex.data.DataService");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.RtmpEndpoint", "flex.messaging.endpoints.RTMPEndpoint");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.AMFEndpoint", "flex.messaging.endpoints.AMFEndpoint");
		Add("Dolo.PlanetAI.NET.Fluorine.Messaging.Services.Remoting.DotNetAdapter", "flex.messaging.services.remoting.adapters.JavaAdapter");
	}

	public void Add(string type, string customClass)
	{
		ClassMapping classMapping = new ClassMapping();
		classMapping.Type = type;
		classMapping.CustomClass = customClass;
		Add(classMapping);
	}

	public override void Add(ClassMapping value)
	{
		_typeToCustomClass[value.Type] = value.CustomClass;
		_customClassToType[value.CustomClass] = value.Type;
		base.Add(value);
	}

	public override void Insert(int index, ClassMapping value)
	{
		_typeToCustomClass[value.Type] = value.CustomClass;
		_customClassToType[value.CustomClass] = value.Type;
		base.Insert(index, value);
	}

	public override bool Remove(ClassMapping value)
	{
		_typeToCustomClass.Remove(value.Type);
		_customClassToType.Remove(value.CustomClass);
		return base.Remove(value);
	}

	public string GetCustomClass(string type)
	{
		if (_typeToCustomClass.ContainsKey(type))
		{
			return _typeToCustomClass[type];
		}
		return type;
	}

	public string GetType(string customClass)
	{
		if (customClass == null)
		{
			return null;
		}
		if (_customClassToType.ContainsKey(customClass))
		{
			return _customClassToType[customClass];
		}
		return customClass;
	}
}
