using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0OptimizedObjectReader : IAMFReader
{
	private Hashtable _optimizedReaders;

	public AMF0OptimizedObjectReader()
	{
		_optimizedReaders = new Hashtable();
	}

	public object ReadData(AMFReader reader)
	{
		object obj = null;
		string text = reader.ReadString();
		IReflectionOptimizer reflectionOptimizer = _optimizedReaders[text] as IReflectionOptimizer;
		if (reflectionOptimizer == null)
		{
			lock (_optimizedReaders)
			{
				if (!_optimizedReaders.Contains(text))
				{
					_optimizedReaders[text] = new AMF0TempObjectReader();
					Type type = ObjectFactory.Locate(text);
					if (type != null)
					{
						obj = ObjectFactory.CreateInstance(type);
						reader.AddReference(obj);
						if (type != null)
						{
							if (reflectionOptimizer != null)
							{
								_optimizedReaders[text] = reflectionOptimizer;
							}
							else
							{
								_optimizedReaders[text] = new AMF0TempObjectReader();
							}
						}
					}
					else
					{
						reflectionOptimizer = new AMF0TypedASObjectReader(text);
						_optimizedReaders[text] = reflectionOptimizer;
						obj = reflectionOptimizer.ReadData(reader, null);
					}
				}
				else
				{
					reflectionOptimizer = _optimizedReaders[text] as IReflectionOptimizer;
					obj = reflectionOptimizer.ReadData(reader, null);
				}
			}
		}
		else
		{
			obj = reflectionOptimizer.ReadData(reader, null);
		}
		return obj;
	}
}
