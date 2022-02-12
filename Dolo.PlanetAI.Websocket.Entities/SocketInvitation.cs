namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketInvitation
{
	public int messageType { get; set; }

	public SocketContentNonInvite messageContent { get; set; }
}
