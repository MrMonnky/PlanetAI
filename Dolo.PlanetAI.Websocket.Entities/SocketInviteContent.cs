namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketInviteContent
{
	public string serializedString { get; set; }

	public string type { get; set; }

	public SocketInviteObject notificationObject { get; set; }

	public SocketInviteObject rawObj { get; set; }

	public object eventName { get; set; }

	public int actorId { get; set; }
}
