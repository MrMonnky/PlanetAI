using System.Collections.Generic;

namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketAutographObject
{
	public object eventName { get; set; }

	public string userName { get; set; }

	public int importance { get; set; }

	public int notificationCatetoryId { get; set; }

	public string notificationTypeId { get; set; }

	public string timestamp { get; set; }

	public string type { get; set; }

	public int systemMessageType { get; set; }

	public string localizedText { get; set; }

	public string iconSubPath { get; set; }

	public List<object> affectedUserIds { get; set; }

	public int fame { get; set; }

	public int userId { get; set; }

	public int actorId { get; set; }

	public string applicationId { get; set; }
}
