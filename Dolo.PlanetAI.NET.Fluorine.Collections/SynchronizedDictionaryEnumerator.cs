using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class SynchronizedDictionaryEnumerator : SynchronizedEnumerator, IDictionaryEnumerator, IEnumerator
{
	protected IDictionaryEnumerator Enumerator => (IDictionaryEnumerator)_enumerator;

	public object Key
	{
		get
		{
			lock (_syncRoot)
			{
				return Enumerator.Key;
			}
		}
	}

	public object Value
	{
		get
		{
			lock (_syncRoot)
			{
				return Enumerator.Value;
			}
		}
	}

	public DictionaryEntry Entry
	{
		get
		{
			lock (_syncRoot)
			{
				return Enumerator.Entry;
			}
		}
	}

	public SynchronizedDictionaryEnumerator(object syncRoot, IDictionaryEnumerator enumerator)
		: base(syncRoot, enumerator)
	{
	}
}
