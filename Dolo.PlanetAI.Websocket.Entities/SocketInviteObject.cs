namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketInviteObject
{
	public string houseOwnerName { get; set; }

	public string roomId { get; set; }

	public int roomIdOnlyId { get; set; }

	public int roomSection { get; set; }

	public int userId { get; set; }

	public string applicationId { get; set; }

	public object eventName { get; set; }

	public int notificationCatetoryId { get; set; }

	public int importance { get; set; }

	public string type { get; set; }

	public string notificationTypeId { get; set; }

	public string iconSubPath { get; set; }

	public string localizedText { get; set; }

	public int actorId { get; set; }

	public string userName { get; set; }
}
