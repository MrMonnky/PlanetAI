namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketAutographContent
{
	public string serializedString { get; set; }

	public string type { get; set; }

	public SocketAutographRawObj rawObj { get; set; }

	public SocketAutographObject notificationObject { get; set; }

	public object eventName { get; set; }

	public int actorId { get; set; }
}
