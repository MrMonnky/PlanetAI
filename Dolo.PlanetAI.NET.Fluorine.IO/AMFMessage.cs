using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFMessage
{
	protected ushort _version = 0;

	protected List<AMFBody> _bodies;

	protected List<AMFHeader> _headers;

	public ushort Version => _version;

	public int BodyCount => _bodies.Count;

	public ReadOnlyCollection<AMFBody> Bodies => _bodies.AsReadOnly();

	public int HeaderCount => _headers.Count;

	public ObjectEncoding ObjectEncoding
	{
		get
		{
			if (_version == 0 || _version == 1)
			{
				return ObjectEncoding.AMF0;
			}
			if (_version == 3)
			{
				return ObjectEncoding.AMF3;
			}
			throw new UnexpectedAMF();
		}
	}

	public AMFMessage()
		: this(0)
	{
	}

	public AMFMessage(ushort version)
	{
		_version = version;
		_headers = new List<AMFHeader>(1);
		_bodies = new List<AMFBody>(1);
	}

	public AMFMessage AddMany(AMFBody body, List<AMFHeader> header)
	{
		_bodies.Add(body);
		_headers.AddRange(header);
		return this;
	}

	public void AddBody(AMFBody body)
	{
		_bodies.Add(body);
	}

	public void AddHeader(AMFHeader header)
	{
		_headers.Add(header);
	}

	public AMFBody GetBodyAt(int index)
	{
		return _bodies[index];
	}

	public AMFHeader GetHeaderAt(int index)
	{
		return _headers[index];
	}

	public AMFHeader GetHeader(string header)
	{
		int num = 0;
		while (_headers != null && num < _headers.Count)
		{
			AMFHeader aMFHeader = _headers[num];
			if (aMFHeader.Name == header)
			{
				return aMFHeader;
			}
			num++;
		}
		return null;
	}

	public void RemoveHeader(string header)
	{
		int num = 0;
		while (_headers != null && num < _headers.Count)
		{
			AMFHeader aMFHeader = _headers[num];
			if (aMFHeader.Name == header)
			{
				_headers.RemoveAt(num);
			}
			num++;
		}
	}
}
