namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class AsyncMessage : MessageBase
{
	public const string SubtopicHeader = "DSSubtopic";

	protected string _correlationId;

	public string correlationId
	{
		get
		{
			return _correlationId;
		}
		set
		{
			_correlationId = value;
		}
	}
}
