using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

[XmlType(TypeName = "settings")]
internal sealed class AMFSettings
{
	private ClassMappingCollection _classMappings;

	private NullableTypeCollection _nullables;

	private ServiceCollection _services;

	private ImportNamespaceCollection _importedNamespaces;

	private HttpCompressSettings _httpCompressSettings;

	private CacheCollection _cache;

	private SchedulingServiceSettings _schedulingServiceSettings;

	private bool _acceptNullValueTypes;

	private RemotingServiceAttributeConstraint _remotingServiceAttributeConstraint;

	private TimezoneCompensation _timezoneCompensation;

	private OptimizerSettings _optimizerSettings;

	private RuntimeSettings _runtimeSettings;

	[XmlArray("classMappings")]
	[XmlArrayItem("classMapping", typeof(ClassMapping))]
	public ClassMappingCollection ClassMappings
	{
		get
		{
			if (_classMappings == null)
			{
				_classMappings = new ClassMappingCollection();
			}
			return _classMappings;
		}
	}

	[XmlArray("nullable")]
	[XmlArrayItem("type", typeof(NullableType))]
	public NullableTypeCollection Nullables
	{
		get
		{
			if (_nullables == null)
			{
				_nullables = new NullableTypeCollection();
			}
			return _nullables;
		}
	}

	[XmlArray("services")]
	[XmlArrayItem("service", typeof(ServiceConfiguration))]
	public ServiceCollection Services
	{
		get
		{
			if (_services == null)
			{
				_services = new ServiceCollection();
			}
			return _services;
		}
	}

	[XmlElement(ElementName = "httpCompress")]
	public HttpCompressSettings HttpCompressSettings
	{
		get
		{
			return _httpCompressSettings;
		}
		set
		{
			_httpCompressSettings = value;
		}
	}

	[XmlArray("cache")]
	[XmlArrayItem("cachedService", typeof(CachedService))]
	public CacheCollection Cache
	{
		get
		{
			if (_cache == null)
			{
				_cache = new CacheCollection();
			}
			return _cache;
		}
	}

	[XmlArray("importNamespaces")]
	[XmlArrayItem("add", typeof(ImportNamespace))]
	public ImportNamespaceCollection ImportNamespaces
	{
		get
		{
			if (_importedNamespaces == null)
			{
				_importedNamespaces = new ImportNamespaceCollection();
			}
			return _importedNamespaces;
		}
	}

	[XmlElement(ElementName = "schedulingService")]
	public SchedulingServiceSettings SchedulingService
	{
		get
		{
			if (_schedulingServiceSettings == null)
			{
				_schedulingServiceSettings = new SchedulingServiceSettings();
			}
			return _schedulingServiceSettings;
		}
		set
		{
			_schedulingServiceSettings = value;
		}
	}

	[XmlElement(ElementName = "timezoneCompensation")]
	public TimezoneCompensation TimezoneCompensation
	{
		get
		{
			return _timezoneCompensation;
		}
		set
		{
			_timezoneCompensation = value;
		}
	}

	[XmlElement(ElementName = "acceptNullValueTypes")]
	public bool AcceptNullValueTypes
	{
		get
		{
			return _acceptNullValueTypes;
		}
		set
		{
			_acceptNullValueTypes = value;
		}
	}

	[XmlElement(ElementName = "remotingServiceAttribute")]
	public RemotingServiceAttributeConstraint RemotingServiceAttribute
	{
		get
		{
			return _remotingServiceAttributeConstraint;
		}
		set
		{
			_remotingServiceAttributeConstraint = value;
		}
	}

	[XmlElement(ElementName = "optimizer")]
	public OptimizerSettings Optimizer
	{
		get
		{
			return _optimizerSettings;
		}
		set
		{
			_optimizerSettings = value;
		}
	}

	[XmlElement(ElementName = "runtime")]
	public RuntimeSettings Runtime
	{
		get
		{
			return _runtimeSettings;
		}
		set
		{
			_runtimeSettings = value;
		}
	}

	public AMFSettings()
	{
		_timezoneCompensation = TimezoneCompensation.None;
		_remotingServiceAttributeConstraint = RemotingServiceAttributeConstraint.Access;
		_acceptNullValueTypes = false;
		_runtimeSettings = new RuntimeSettings();
	}
}
