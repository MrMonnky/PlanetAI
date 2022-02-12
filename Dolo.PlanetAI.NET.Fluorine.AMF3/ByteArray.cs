using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

[TypeConverter(typeof(ByteArrayConverter))]
internal class ByteArray : IDataInput, IDataOutput
{
	private MemoryStream _memoryStream;

	private DataOutput _dataOutput;

	private DataInput _dataInput;

	private ObjectEncoding _objectEncoding;

	public uint Length => (uint)_memoryStream.Length;

	public int Position
	{
		get
		{
			return (int)_memoryStream.Position;
		}
		set
		{
			_memoryStream.Position = value;
		}
	}

	public ObjectEncoding ObjectEncoding
	{
		get
		{
			return _objectEncoding;
		}
		set
		{
			_objectEncoding = value;
			_dataInput.ObjectEncoding = value;
			_dataOutput.ObjectEncoding = value;
		}
	}

	internal MemoryStream MemoryStream => _memoryStream;

	public ByteArray()
	{
		_memoryStream = new MemoryStream();
		AMFReader amfReader = new AMFReader(_memoryStream);
		AMFWriter aMFWriter = new AMFWriter();
		aMFWriter.SetBinary(_memoryStream);
		_dataOutput = new DataOutput(aMFWriter);
		_dataInput = new DataInput(amfReader);
		_objectEncoding = ObjectEncoding.AMF3;
	}

	public ByteArray(MemoryStream ms)
	{
		_memoryStream = ms;
		AMFReader amfReader = new AMFReader(_memoryStream);
		AMFWriter amfWriter = new AMFWriter();
		_dataOutput = new DataOutput(amfWriter);
		_dataInput = new DataInput(amfReader);
		_objectEncoding = ObjectEncoding.AMF3;
	}

	internal ByteArray(byte[] buffer)
	{
		_memoryStream = new MemoryStream();
		_memoryStream.Write(buffer, 0, buffer.Length);
		_memoryStream.Position = 0L;
		AMFReader amfReader = new AMFReader(_memoryStream);
		AMFWriter amfWriter = new AMFWriter();
		_dataOutput = new DataOutput(amfWriter);
		_dataInput = new DataInput(amfReader);
		_objectEncoding = ObjectEncoding.AMF3;
	}

	public byte[] GetBuffer()
	{
		return _memoryStream.GetBuffer();
	}

	public bool ReadBoolean()
	{
		return _dataInput.ReadBoolean();
	}

	public byte ReadByte()
	{
		return _dataInput.ReadByte();
	}

	public void ReadBytes(byte[] bytes, uint offset, uint length)
	{
		_dataInput.ReadBytes(bytes, offset, length);
	}

	public double ReadDouble()
	{
		return _dataInput.ReadDouble();
	}

	public float ReadFloat()
	{
		return _dataInput.ReadFloat();
	}

	public int ReadInt()
	{
		return _dataInput.ReadInt();
	}

	public object ReadObject()
	{
		return _dataInput.ReadObject();
	}

	public short ReadShort()
	{
		return _dataInput.ReadShort();
	}

	public byte ReadUnsignedByte()
	{
		return _dataInput.ReadUnsignedByte();
	}

	public uint ReadUnsignedInt()
	{
		return _dataInput.ReadUnsignedInt();
	}

	public ushort ReadUnsignedShort()
	{
		return _dataInput.ReadUnsignedShort();
	}

	public string ReadUTF()
	{
		return _dataInput.ReadUTF();
	}

	public string ReadUTFBytes(uint length)
	{
		return _dataInput.ReadUTFBytes(length);
	}

	public void WriteBoolean(bool value)
	{
		_dataOutput.WriteBoolean(value);
	}

	public void WriteByte(byte value)
	{
		_dataOutput.WriteByte(value);
	}

	public void WriteBytes(byte[] bytes, int offset, int length)
	{
		_dataOutput.WriteBytes(bytes, offset, length);
	}

	public void WriteDouble(double value)
	{
		_dataOutput.WriteDouble(value);
	}

	public void WriteFloat(float value)
	{
		_dataOutput.WriteFloat(value);
	}

	public void WriteInt(int value)
	{
		_dataOutput.WriteInt(value);
	}

	public void WriteObject(object value)
	{
		_dataOutput.WriteObject(value);
	}

	public void WriteShort(short value)
	{
		_dataOutput.WriteShort(value);
	}

	public void WriteUnsignedInt(uint value)
	{
		_dataOutput.WriteUnsignedInt(value);
	}

	public void WriteUTF(string value)
	{
		_dataOutput.WriteUTF(value);
	}

	public void WriteUTFBytes(string value)
	{
		_dataOutput.WriteUTFBytes(value);
	}

	public void Compress()
	{
		byte[] buffer = _memoryStream.GetBuffer();
		DeflateStream deflateStream = new DeflateStream(_memoryStream, CompressionMode.Compress, leaveOpen: true);
		deflateStream.Write(buffer, 0, buffer.Length);
		deflateStream.Close();
		AMFReader amfReader = new AMFReader(_memoryStream);
		AMFWriter amfWriter = new AMFWriter();
		_dataOutput = new DataOutput(amfWriter);
		_dataInput = new DataInput(amfReader);
	}

	public void Uncompress()
	{
		DeflateStream deflateStream = new DeflateStream(_memoryStream, CompressionMode.Decompress, leaveOpen: false);
		MemoryStream memoryStream = new MemoryStream();
		byte[] array = new byte[1024];
		_memoryStream.ReadByte();
		_memoryStream.ReadByte();
		while (true)
		{
			int num = deflateStream.Read(array, 0, array.Length);
			if (num > 0)
			{
				memoryStream.Write(array, 0, num);
				continue;
			}
			break;
		}
		deflateStream.Close();
		_memoryStream.Close();
		_memoryStream.Dispose();
		_memoryStream = memoryStream;
		_memoryStream.Position = 0L;
		AMFReader amfReader = new AMFReader(_memoryStream);
		AMFWriter amfWriter = new AMFWriter();
		_dataOutput = new DataOutput(amfWriter);
		_dataInput = new DataInput(amfReader);
	}
}
