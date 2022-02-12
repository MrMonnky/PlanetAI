using System.Threading;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal class AtomicInteger
{
	private int _counter;

	public int Value
	{
		get
		{
			return _counter;
		}
		set
		{
			Interlocked.Exchange(ref _counter, value);
		}
	}

	public AtomicInteger()
		: this(0)
	{
	}

	public AtomicInteger(int initialValue)
	{
		_counter = initialValue;
	}

	public int Increment()
	{
		return Interlocked.Increment(ref _counter);
	}

	public int Decrement()
	{
		return Interlocked.Decrement(ref _counter);
	}

	public int PostDecrement()
	{
		return Interlocked.Decrement(ref _counter) + 1;
	}

	public int PostIncrement()
	{
		return Interlocked.Increment(ref _counter) - 1;
	}

	public int Increment(int delta)
	{
		return Interlocked.Add(ref _counter, delta);
	}

	public int Decrement(int delta)
	{
		return Interlocked.Add(ref _counter, -delta);
	}

	public int PostDecrement(int delta)
	{
		return Interlocked.Add(ref _counter, -delta) + delta;
	}

	public int PostIncrement(int delta)
	{
		return Interlocked.Add(ref _counter, delta) - delta;
	}

	public override string ToString()
	{
		return Value.ToString();
	}
}
