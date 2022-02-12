namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal interface IMessageComponent
{
	void OnOOBControlMessage(IMessageComponent source, IPipe pipe, OOBControlMessage oobCtrlMsg);
}
