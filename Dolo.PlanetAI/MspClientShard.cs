using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public class MspClientShard
{
	private readonly List<Action<MspConfig>> mspConfigs;

	private readonly List<MspClient> mspClients;

	private readonly List<MspClient> mspWorkedClients;

	private readonly List<MspClient> mspFailedClients;

	public int Worked => mspWorkedClients.Count;

	public int Failed => mspFailedClients.Count;

	public List<MspClient> GetWorked => mspWorkedClients;

	public List<MspClient> GetFailed => mspFailedClients;

	public MspClientShard(params Action<MspConfig>[] configs)
	{
		mspWorkedClients = new List<MspClient>();
		mspFailedClients = new List<MspClient>();
		mspClients = new List<MspClient>();
		mspConfigs = new List<Action<MspConfig>>();
		mspConfigs.AddRange(configs);
	}

	public async Task<List<MspClient>> LoginAsync()
	{
		foreach (Action<MspConfig> config in mspConfigs)
		{
			mspClients.Add(new MspClient(config));
		}
		List<Task> tasks = new List<Task>();
		foreach (MspClient client in mspClients)
		{
			List<Task> list = tasks;
			list.Add(await Task.Factory.StartNew((Func<Task>)async delegate
			{
				if ((await client.LoginAsync()).Success)
				{
					mspWorkedClients.Add(client);
				}
				else
				{
					mspFailedClients.Add(client);
				}
			}));
		}
		await Task.WhenAll(tasks);
		return mspWorkedClients;
	}
}
