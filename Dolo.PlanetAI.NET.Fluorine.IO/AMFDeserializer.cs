using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFDeserializer : AMFReader
{
	private List<AMFBody> _failedAMFBodies = new List<AMFBody>(1);

	public AMFBody[] FailedAMFBodies => _failedAMFBodies.ToArray();

	public AMFDeserializer(Stream stream)
		: base(stream)
	{
		base.FaultTolerancy = true;
	}

	public AMFMessage ReadAMFMessage()
	{
		try
		{
			ushort version = ReadUInt16();
			AMFMessage aMFMessage = new AMFMessage(version);
			int num = ReadUInt16();
			for (int i = 0; i < num; i++)
			{
				aMFMessage.AddHeader(ReadHeader());
			}
			int num2 = ReadUInt16();
			for (int j = 0; j < num2; j++)
			{
				AMFBody aMFBody = ReadBody();
				if (aMFBody != null)
				{
					aMFMessage.AddBody(aMFBody);
				}
			}
			return aMFMessage;
		}
		catch
		{
			return new AMFMessage();
		}
	}

	private AMFHeader ReadHeader()
	{
		Reset();
		string name = ReadString();
		bool mustUnderstand = ReadBoolean();
		int num = ReadInt32();
		object content = ReadData();
		return new AMFHeader(name, mustUnderstand, content);
	}

	private AMFBody ReadBody()
	{
		Reset();
		string target = ReadString();
		string response = ReadString();
		int num = ReadInt32();
		if (base.BaseStream.CanSeek)
		{
			long position = base.BaseStream.Position;
			try
			{
				object content = ReadData();
				AMFBody aMFBody = new AMFBody(target, response, content);
				Exception lastError = base.LastError;
				if (lastError != null)
				{
					ErrorResponseBody errorBody = GetErrorBody(aMFBody, lastError);
					_failedAMFBodies.Add(errorBody);
					return null;
				}
				return aMFBody;
			}
			catch (Exception exception)
			{
				base.BaseStream.Position = position + num;
				AMFBody amfBody = new AMFBody(target, response, null);
				ErrorResponseBody errorBody2 = GetErrorBody(amfBody, exception);
				_failedAMFBodies.Add(errorBody2);
				return null;
			}
		}
		try
		{
			object content2 = ReadData();
			AMFBody aMFBody2 = new AMFBody(target, response, content2);
			Exception lastError2 = base.LastError;
			if (lastError2 != null)
			{
				ErrorResponseBody errorBody3 = GetErrorBody(aMFBody2, lastError2);
				_failedAMFBodies.Add(errorBody3);
				return null;
			}
			return aMFBody2;
		}
		catch (Exception exception2)
		{
			AMFBody amfBody2 = new AMFBody(target, response, null);
			ErrorResponseBody errorBody4 = GetErrorBody(amfBody2, exception2);
			_failedAMFBodies.Add(errorBody4);
			throw;
		}
	}

	private ErrorResponseBody GetErrorBody(AMFBody amfBody, Exception exception)
	{
		ErrorResponseBody errorResponseBody = null;
		try
		{
			object obj = amfBody.Content;
			if (obj is IList)
			{
				obj = (obj as IList)[0];
			}
			if (obj is IMessage)
			{
				return new ErrorResponseBody(amfBody, obj as IMessage, exception);
			}
			return new ErrorResponseBody(amfBody, exception);
		}
		catch
		{
			return new ErrorResponseBody(amfBody, exception);
		}
	}
}
