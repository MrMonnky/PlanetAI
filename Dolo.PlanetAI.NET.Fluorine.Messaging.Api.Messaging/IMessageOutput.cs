using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal interface IMessageOutput
{
	void PushMessage(IMessage message);

	bool Subscribe(IProvider provider, Dictionary<string, object> parameterMap);

	bool Unsubscribe(IProvider provider);

	ICollection GetProviders();

	void SendOOBControlMessage(IProvider provider, OOBControlMessage oobCtrlMsg);
}
