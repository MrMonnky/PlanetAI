namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketContentNonInvite
{
	public SocketInviteContent note { get; set; }

	public string targetUserId { get; set; }

	public string actorId { get; set; }
}
