using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.IO.Bytecode;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3OptimizedObjectReader : IAMFReader
{
	private Hashtable _optimizedReaders;

	public AMF3OptimizedObjectReader()
	{
		_optimizedReaders = new Hashtable();
	}

	public object ReadData(AMFReader reader)
	{
		int num = reader.ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (!flag)
		{
			return reader.ReadAMF3ObjectReference(num);
		}
		ClassDefinition classDefinition = reader.ReadClassDefinition(num);
		object obj = null;
		IReflectionOptimizer reflectionOptimizer = _optimizedReaders[classDefinition.ClassName] as IReflectionOptimizer;
		if (reflectionOptimizer == null)
		{
			lock (_optimizedReaders)
			{
				if (classDefinition.IsTypedObject)
				{
					if (!_optimizedReaders.Contains(classDefinition.ClassName))
					{
						_optimizedReaders[classDefinition.ClassName] = new AMF3TempObjectReader();
						Type type = ObjectFactory.Locate(classDefinition.ClassName);
						if (type != null)
						{
							obj = ObjectFactory.CreateInstance(type);
							if (classDefinition.IsExternalizable)
							{
								reflectionOptimizer = new AMF3ExternalizableReader();
								_optimizedReaders[classDefinition.ClassName] = reflectionOptimizer;
								obj = reflectionOptimizer.ReadData(reader, classDefinition);
							}
							else
							{
								reader.AddAMF3ObjectReference(obj);
								if (reflectionOptimizer != null)
								{
									_optimizedReaders[classDefinition.ClassName] = reflectionOptimizer;
								}
								else
								{
									_optimizedReaders[classDefinition.ClassName] = new AMF3TempObjectReader();
								}
							}
						}
						else
						{
							reflectionOptimizer = new AMF3TypedASObjectReader(classDefinition.ClassName);
							_optimizedReaders[classDefinition.ClassName] = reflectionOptimizer;
							obj = reflectionOptimizer.ReadData(reader, classDefinition);
						}
					}
					else
					{
						reflectionOptimizer = _optimizedReaders[classDefinition.ClassName] as IReflectionOptimizer;
						obj = reflectionOptimizer.ReadData(reader, classDefinition);
					}
				}
				else
				{
					reflectionOptimizer = new AMF3TypedASObjectReader(classDefinition.ClassName);
					_optimizedReaders[classDefinition.ClassName] = reflectionOptimizer;
					obj = reflectionOptimizer.ReadData(reader, classDefinition);
				}
			}
		}
		else
		{
			obj = reflectionOptimizer.ReadData(reader, classDefinition);
		}
		return obj;
	}
}
