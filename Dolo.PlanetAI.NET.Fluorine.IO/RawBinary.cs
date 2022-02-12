namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class RawBinary
{
	private byte[] _buffer;

	public byte[] Buffer => _buffer;

	public RawBinary(byte[] buffer)
	{
		_buffer = buffer;
	}
}
