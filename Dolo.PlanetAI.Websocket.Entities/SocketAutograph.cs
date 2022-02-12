namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketAutograph
{
	public string targetUserId { get; set; }

	public int messageType { get; set; }

	public SocketAutographContent messageContent { get; set; }
}
