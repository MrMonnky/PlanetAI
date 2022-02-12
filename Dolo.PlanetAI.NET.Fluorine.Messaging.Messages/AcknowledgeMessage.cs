using System;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class AcknowledgeMessage : AsyncMessage
{
	public AcknowledgeMessage()
	{
		_messageId = Guid.NewGuid().ToString("D");
		_timestamp = Environment.TickCount;
	}
}
