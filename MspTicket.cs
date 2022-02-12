namespace Dolo.PlanetAI;

public class MspTicket
{
	public string Ticket { get; internal set; }

	public object[] anyAttribute { get; internal set; }

	internal MspTicket(string ticket = "")
	{
		Ticket = ticket;
	}
}
