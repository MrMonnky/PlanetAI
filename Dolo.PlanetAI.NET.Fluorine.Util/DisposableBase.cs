using System;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal class DisposableBase : IDisposable
{
	private volatile bool _disposed = false;

	public bool IsDisposed => _disposed;

	~DisposableBase()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				Free();
			}
			FreeUnmanaged();
			_disposed = true;
		}
	}

	protected virtual void Free()
	{
	}

	protected virtual void FreeUnmanaged()
	{
	}

	protected virtual void CheckDisposed()
	{
		if (_disposed)
		{
			throw new ObjectDisposedException(null);
		}
	}
}
