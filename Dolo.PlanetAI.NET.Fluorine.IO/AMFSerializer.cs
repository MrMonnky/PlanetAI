using System;
using System.Collections;
using System.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFSerializer : AMFWriter
{
	private MemoryStream Stream;

	public AMFSerializer(AMFMessage message)
	{
		Stream = new MemoryStream();
		SetBinary(Stream);
		WriteMessage(message);
		BinaryWriter.Flush();
		BinaryWriter.Close();
	}

	internal AMFSerializer(AMFWriter writer, Stream stream)
		: base(writer, stream)
	{
	}

	public byte[] GetData()
	{
		return Stream.ToArray();
	}

	public void WriteMessage(AMFMessage amfMessage)
	{
		try
		{
			WriteShort(amfMessage.Version);
			int headerCount = amfMessage.HeaderCount;
			WriteShort(headerCount);
			for (int i = 0; i < headerCount; i++)
			{
				WriteHeader(amfMessage.GetHeaderAt(i), ObjectEncoding.AMF0);
			}
			int bodyCount = amfMessage.BodyCount;
			WriteShort(bodyCount);
			for (int j = 0; j < bodyCount; j++)
			{
				if (amfMessage.GetBodyAt(j) is ResponseBody responseBody && !responseBody.IgnoreResults)
				{
					if (Stream.CanSeek)
					{
						long position = Stream.Position;
						try
						{
							responseBody.WriteBody(amfMessage.ObjectEncoding, this);
						}
						catch (Exception ex)
						{
							Stream.Seek(position, SeekOrigin.Begin);
							ErrorResponseBody errorResponseBody;
							if (responseBody.RequestBody.IsEmptyTarget)
							{
								object obj = responseBody.RequestBody.Content;
								if (obj is IList)
								{
									obj = (obj as IList)[0];
								}
								IMessage message = obj as IMessage;
								MessageException ex2 = new MessageException(ex);
								ex2.FaultCode = __Res.GetString("Amf_SerializationFail");
								errorResponseBody = new ErrorResponseBody(responseBody.RequestBody, message, ex2);
							}
							else
							{
								errorResponseBody = new ErrorResponseBody(responseBody.RequestBody, ex);
							}
							try
							{
								errorResponseBody.WriteBody(amfMessage.ObjectEncoding, this);
							}
							catch (Exception)
							{
								throw;
							}
						}
					}
					else
					{
						responseBody.WriteBody(amfMessage.ObjectEncoding, this);
					}
				}
				else
				{
					AMFBody bodyAt = amfMessage.GetBodyAt(j);
					ValidationUtils.ObjectNotNull(bodyAt, "amfBody");
					bodyAt.WriteBody(amfMessage.ObjectEncoding, this);
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void WriteHeader(AMFHeader header, ObjectEncoding objectEncoding)
	{
		Reset();
		WriteUTF(header.Name);
		WriteBoolean(header.MustUnderstand);
		WriteInt32(-1);
		WriteData(objectEncoding, header.Content);
	}
}
