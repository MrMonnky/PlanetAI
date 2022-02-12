using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

internal abstract class ServiceAdapter
{
	private object _syncLock = new object();

	private Destination _destination;

	private DestinationSettings _destinationSettings;

	private AdapterSettings _adapterSettings;

	public virtual bool HandlesSubscriptions => false;

	public Destination Destination => _destination;

	public DestinationSettings DestinationSettings => _destinationSettings;

	public AdapterSettings AdapterSettings => _adapterSettings;

	public object SyncRoot => _syncLock;

	public virtual Task<object> Invoke(IMessage message)
	{
		return Task.FromResult<object>(null);
	}

	public virtual object Manage(CommandMessage commandMessage)
	{
		return new AcknowledgeMessage();
	}

	public virtual void Init()
	{
	}

	public virtual void Stop()
	{
	}

	internal void SetDestination(Destination value)
	{
		_destination = value;
	}

	internal void SetDestinationSettings(DestinationSettings value)
	{
		_destinationSettings = value;
	}

	internal void SetAdapterSettings(AdapterSettings value)
	{
		_adapterSettings = value;
	}
}
