using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class SynchronizedEnumerator : IEnumerator
{
	protected object _syncRoot;

	protected IEnumerator _enumerator;

	public object Current
	{
		get
		{
			lock (_syncRoot)
			{
				return _enumerator.Current;
			}
		}
	}

	public SynchronizedEnumerator(object syncRoot, IEnumerator enumerator)
	{
		_syncRoot = syncRoot;
		_enumerator = enumerator;
	}

	public bool MoveNext()
	{
		lock (_syncRoot)
		{
			return _enumerator.MoveNext();
		}
	}

	public void Reset()
	{
		lock (_syncRoot)
		{
			_enumerator.Reset();
		}
	}
}
