using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dolo.PlanetAI.Websocket.NET;

internal class MspSocket
{
	private ClientWebSocket WebSocket;

	private MspChat Chat;

	public event EventHandler<string> OnDataReceived;

	public event EventHandler<string> OnDataSent;

	public event EventHandler<string> OnDataFailed;

	public MspSocket(MspChat chat)
	{
		WebSocket = new ClientWebSocket();
		Chat = chat;
	}

	public MspSocket()
	{
		WebSocket = new ClientWebSocket();
	}

	public async Task ConnectAsync(Uri uri)
	{
		if (WebSocket.State == WebSocketState.Open || WebSocket.State == WebSocketState.Aborted)
		{
			return;
		}
		await WebSocket.ConnectAsync(uri, CancellationToken.None);
		if (WebSocket.State == WebSocketState.Open)
		{
			await Task.Factory.StartNew((Func<Task>)async delegate
			{
				await ReadAsync();
				await HandshakeAsync();
			});
		}
		else
		{
			this.OnDataFailed?.Invoke(null, WebSocket.CloseStatusDescription);
		}
	}

	public async Task CloseAsync()
	{
		await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "closing requested", CancellationToken.None);
		WebSocket = new ClientWebSocket();
	}

	public async Task SendAsync(string message)
	{
		this.OnDataSent?.Invoke(null, message);
		byte[] buffer = Encoding.UTF8.GetBytes(message);
		if (WebSocket.State == WebSocketState.Open)
		{
			await WebSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
		}
		else
		{
			this.OnDataFailed?.Invoke(null, WebSocket.CloseStatusDescription);
		}
	}

	private async Task HandshakeAsync()
	{
		while (WebSocket.State == WebSocketState.Open)
		{
			await Chat.PingAsync();
			await Task.Delay(5000);
		}
	}

	private async Task ReadAsync()
	{
		byte[] buffer = new byte[32768];
		while (WebSocket.State == WebSocketState.Open)
		{
			WebSocketReceiveResult socketResult = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			if (socketResult.MessageType == WebSocketMessageType.Close)
			{
				await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "closing requested", CancellationToken.None);
			}
			else if (socketResult.MessageType == WebSocketMessageType.Text)
			{
				this.OnDataReceived?.Invoke(null, Encoding.UTF8.GetString(buffer, 0, socketResult.Count));
			}
		}
	}
}
