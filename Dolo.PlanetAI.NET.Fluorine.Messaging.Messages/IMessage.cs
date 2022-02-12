using System;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal interface IMessage : ICloneable
{
	object clientId { get; set; }

	string destination { get; set; }

	string messageId { get; set; }

	long timestamp { get; set; }

	long timeToLive { get; set; }

	object body { get; set; }

	Dictionary<string, object> headers { get; set; }

	object GetHeader(string name);

	void SetHeader(string name, object value);

	bool HeaderExists(string name);
}
