using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class HttpCompressSettings
{
	private MimeTypeEntryCollection _excludedTypes;

	private PathEntryCollection _excludedPaths;

	private int _threshold;

	public static HttpCompressSettings Default => new HttpCompressSettings();

	[XmlElement(ElementName = "threshold")]
	public int Threshold
	{
		get
		{
			return _threshold;
		}
		set
		{
			_threshold = value;
		}
	}

	[XmlArray("excludedMimeTypes")]
	[XmlArrayItem("add", typeof(MimeTypeEntry))]
	public MimeTypeEntryCollection ExcludedMimeTypes
	{
		get
		{
			if (_excludedTypes == null)
			{
				_excludedTypes = new MimeTypeEntryCollection();
			}
			return _excludedTypes;
		}
	}

	[XmlArray("excludedPaths")]
	[XmlArrayItem("add", typeof(PathEntry))]
	public PathEntryCollection ExcludedPaths
	{
		get
		{
			if (_excludedPaths == null)
			{
				_excludedPaths = new PathEntryCollection();
			}
			return _excludedPaths;
		}
	}

	public HttpCompressSettings()
	{
		_excludedTypes = new MimeTypeEntryCollection();
		_excludedPaths = new PathEntryCollection();
		_threshold = 20480;
	}

	public bool IsExcludedMimeType(string mimetype)
	{
		return _excludedTypes.Contains(mimetype.ToLower());
	}

	public bool IsExcludedPath(string relUrl)
	{
		return _excludedPaths.Contains(relUrl.ToLower());
	}
}
