using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

[XmlType(TypeName = "service")]
internal sealed class ServiceConfiguration
{
	private string _name;

	private string _serviceLocation;

	private RemoteMethodCollection _remoteMethodCollection;

	[XmlElement(DataType = "string", ElementName = "name")]
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	[XmlElement(DataType = "string", ElementName = "service-location")]
	public string ServiceLocation
	{
		get
		{
			return _serviceLocation;
		}
		set
		{
			_serviceLocation = value;
		}
	}

	[XmlArray("methods")]
	[XmlArrayItem("remote-method", typeof(RemoteMethod))]
	public RemoteMethodCollection Methods
	{
		get
		{
			if (_remoteMethodCollection == null)
			{
				_remoteMethodCollection = new RemoteMethodCollection();
			}
			return _remoteMethodCollection;
		}
	}
}
