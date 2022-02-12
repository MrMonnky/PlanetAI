using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal interface IMessageInput
{
	IMessage PullMessage();

	IMessage PullMessage(long wait);

	bool Subscribe(IConsumer consumer, Dictionary<string, object> parameterMap);

	bool Unsubscribe(IConsumer consumer);

	ICollection GetConsumers();

	void SendOOBControlMessage(IConsumer consumer, OOBControlMessage oobCtrlMsg);
}
