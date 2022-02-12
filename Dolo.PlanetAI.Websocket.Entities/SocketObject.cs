namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketObject
{
	public int userId { get; set; }

	public string userName { get; set; }

	public string notificationTypeId { get; set; }

	public string applicationId { get; set; }

	public int notificationCatetoryId { get; set; }

	public int importance { get; set; }

	public string iconSubPath { get; set; }

	public string localizedText { get; set; }
}
