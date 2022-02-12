using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal class DataInput : IDataInput
{
	private AMFReader _amfReader;

	private ObjectEncoding _objectEncoding;

	public ObjectEncoding ObjectEncoding
	{
		get
		{
			return _objectEncoding;
		}
		set
		{
			_objectEncoding = value;
		}
	}

	public DataInput(AMFReader amfReader)
	{
		_amfReader = amfReader;
		_objectEncoding = ObjectEncoding.AMF3;
	}

	public bool ReadBoolean()
	{
		return _amfReader.ReadBoolean();
	}

	public byte ReadByte()
	{
		return _amfReader.ReadByte();
	}

	public void ReadBytes(byte[] bytes, uint offset, uint length)
	{
		byte[] array = _amfReader.ReadBytes((int)length);
		for (int i = 0; i < array.Length; i++)
		{
			bytes[i + offset] = array[i];
		}
	}

	public double ReadDouble()
	{
		return _amfReader.ReadDouble();
	}

	public float ReadFloat()
	{
		return _amfReader.ReadFloat();
	}

	public int ReadInt()
	{
		return _amfReader.ReadInt32();
	}

	public object ReadObject()
	{
		object result = null;
		if (_objectEncoding == ObjectEncoding.AMF0)
		{
			result = _amfReader.ReadData();
		}
		if (_objectEncoding == ObjectEncoding.AMF3)
		{
			result = _amfReader.ReadAMF3Data();
		}
		return result;
	}

	public short ReadShort()
	{
		return _amfReader.ReadInt16();
	}

	public byte ReadUnsignedByte()
	{
		return _amfReader.ReadByte();
	}

	public uint ReadUnsignedInt()
	{
		return (uint)_amfReader.ReadInt32();
	}

	public ushort ReadUnsignedShort()
	{
		return _amfReader.ReadUInt16();
	}

	public string ReadUTF()
	{
		return _amfReader.ReadString();
	}

	public string ReadUTFBytes(uint length)
	{
		return _amfReader.ReadUTF((int)length);
	}
}
