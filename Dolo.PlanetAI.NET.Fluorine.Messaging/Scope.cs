using System;
using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class Scope : BasicScope, IScope, IBasicScope, ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable, IServiceContainer, Api.IServiceProvider
{
	private sealed class PrefixFilteringStringEnumerator : IEnumerator
	{
		private string _prefix;

		private int _index;

		private object[] _enumerable = null;

		private string _currentElement;

		public string Current
		{
			get
			{
				if (_index == -1)
				{
					throw new InvalidOperationException("Enum not started.");
				}
				if (_index >= _enumerable.Length)
				{
					throw new InvalidOperationException("Enumeration ended.");
				}
				return _currentElement;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				if (_index == -1)
				{
					throw new InvalidOperationException("Enum not started.");
				}
				if (_index >= _enumerable.Length)
				{
					throw new InvalidOperationException("Enumeration ended.");
				}
				return _currentElement;
			}
		}

		internal PrefixFilteringStringEnumerator(ICollection enumerable, string prefix)
		{
			_prefix = prefix;
			_index = -1;
			_enumerable = new object[enumerable.Count];
			enumerable.CopyTo(_enumerable, 0);
		}

		public void Reset()
		{
			_currentElement = null;
			_index = -1;
		}

		public bool MoveNext()
		{
			while (_index < _enumerable.Length - 1)
			{
				_index++;
				string text = _enumerable[_index] as string;
				if (text.StartsWith(_prefix))
				{
					_currentElement = text;
					return true;
				}
			}
			_index = _enumerable.Length;
			return false;
		}
	}

	private sealed class ConnectionIterator : IEnumerator
	{
		private IEnumerator _connectionIterator;

		private IDictionaryEnumerator _setIterator;

		public object Current => _connectionIterator.Current;

		public ConnectionIterator(Scope scope)
		{
			_setIterator = scope._clients.GetEnumerator();
		}

		public bool MoveNext()
		{
			if (_connectionIterator != null && _connectionIterator.MoveNext())
			{
				return true;
			}
			if (!_setIterator.MoveNext())
			{
				return false;
			}
			for (_connectionIterator = (_setIterator.Value as CopyOnWriteArraySet<IConnection>).GetEnumerator(); _connectionIterator != null; _connectionIterator = (_setIterator.Value as CopyOnWriteArraySet<IConnection>).GetEnumerator())
			{
				if (_connectionIterator.MoveNext())
				{
					return true;
				}
				if (!_setIterator.MoveNext())
				{
					return false;
				}
			}
			return false;
		}

		public void Reset()
		{
			_connectionIterator = null;
			_setIterator.Reset();
		}
	}

	private static string ScopeType = "scope";

	public static string Separator = ":";

	private IScopeContext _context;

	private IScopeHandler _handler;

	private bool _autoStart = true;

	private bool _enabled = true;

	private bool _running = false;

	protected ServiceContainer _serviceContainer;

	private Dictionary<string, IBasicScope> _children = new Dictionary<string, IBasicScope>();

	private Dictionary<IClient, CopyOnWriteArraySet<IConnection>> _clients = new Dictionary<IClient, CopyOnWriteArraySet<IConnection>>();

	public bool IsEnabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			_enabled = value;
		}
	}

	public bool IsRunning => _running;

	public bool AutoStart
	{
		get
		{
			return _autoStart;
		}
		set
		{
			_autoStart = value;
		}
	}

	public bool HasContext => _context != null;

	public IScopeContext Context
	{
		get
		{
			if (!HasContext && base.HasParent)
			{
				return Parent.Context;
			}
			return _context;
		}
		set
		{
			_context = value;
		}
	}

	public bool HasHandler => _handler != null || (base.HasParent && Parent.HasHandler);

	public IScopeHandler Handler
	{
		get
		{
			if (_handler != null)
			{
				return _handler;
			}
			if (base.HasParent)
			{
				return Parent.Handler;
			}
			return null;
		}
		set
		{
			_handler = value;
			if (_handler is IScopeAware)
			{
				(_handler as IScopeAware).SetScope(this);
			}
		}
	}

	public virtual string ContextPath
	{
		get
		{
			if (HasContext)
			{
				return string.Empty;
			}
			if (base.HasParent)
			{
				return Parent.ContextPath + "/" + Name;
			}
			return null;
		}
	}

	protected Scope()
		: this(string.Empty)
	{
	}

	protected Scope(string name)
		: this(name, null)
	{
	}

	protected Scope(string name, Api.IServiceProvider serviceProvider)
		: base(null, ScopeType, name, persistent: false)
	{
		_serviceContainer = new ServiceContainer(serviceProvider);
	}

	public void Init()
	{
		if ((!base.HasParent || Parent.HasChildScope(Name) || Parent.AddChildScope(this)) && AutoStart)
		{
			Start();
		}
	}

	public void Uninit()
	{
		foreach (IBasicScope value in _children.Values)
		{
			if (value is Scope)
			{
				((Scope)value).Uninit();
			}
		}
		Stop();
		if (base.HasParent && Parent.HasChildScope(Name))
		{
			Parent.RemoveChildScope(this);
		}
	}

	public void AddService(Type serviceType, object service)
	{
		_serviceContainer.AddService(serviceType, service);
	}

	public void AddService(Type serviceType, object service, bool promote)
	{
		_serviceContainer.AddService(serviceType, service, promote);
	}

	public void RemoveService(Type serviceType)
	{
		_serviceContainer.RemoveService(serviceType);
	}

	public void RemoveService(Type serviceType, bool promote)
	{
		_serviceContainer.RemoveService(serviceType, promote);
	}

	public virtual object GetService(Type serviceType)
	{
		return _serviceContainer.GetService(serviceType);
	}

	public bool Start()
	{
		lock (base.SyncRoot)
		{
			bool flag = false;
			if (IsEnabled && !IsRunning)
			{
				if (HasHandler)
				{
					try
					{
						if (_handler != null)
						{
							flag = _handler.Start(this);
						}
					}
					catch (Exception)
					{
					}
				}
				else
				{
					flag = true;
				}
				_running = flag;
			}
			return flag;
		}
	}

	public void Stop()
	{
		lock (base.SyncRoot)
		{
			if (IsEnabled && IsRunning && HasHandler)
			{
				try
				{
					if (_handler != null)
					{
						_handler.Stop(this);
					}
				}
				catch (Exception)
				{
				}
			}
			_serviceContainer.Shutdown();
			_running = false;
		}
	}

	protected override void Free()
	{
		if (base.HasParent)
		{
			Parent.RemoveChildScope(this);
		}
		if (HasHandler)
		{
			Handler.Stop(this);
		}
	}

	public bool Connect(IConnection connection)
	{
		return Connect(connection, null);
	}

	public bool Connect(IConnection connection, object[] parameters)
	{
		if (base.HasParent && !Parent.Connect(connection, parameters))
		{
			return false;
		}
		if (HasHandler && !Handler.Connect(connection, this, parameters))
		{
			return false;
		}
		IClient client = connection.Client;
		if (!connection.IsConnected)
		{
			return false;
		}
		if (HasHandler && !Handler.Join(client, this))
		{
			return false;
		}
		if (!connection.IsConnected)
		{
			return false;
		}
		CopyOnWriteArraySet<IConnection> copyOnWriteArraySet = null;
		if (_clients.ContainsKey(client))
		{
			copyOnWriteArraySet = _clients[client];
		}
		else
		{
			copyOnWriteArraySet = new CopyOnWriteArraySet<IConnection>();
			_clients[client] = copyOnWriteArraySet;
		}
		copyOnWriteArraySet.Add(connection);
		AddEventListener(connection);
		return true;
	}

	public void Disconnect(IConnection connection)
	{
		IClient client = connection.Client;
		if (_clients.ContainsKey(client))
		{
			CopyOnWriteArraySet<IConnection> copyOnWriteArraySet = _clients[client];
			copyOnWriteArraySet.Remove(connection);
			IScopeHandler scopeHandler = null;
			if (HasHandler)
			{
				scopeHandler = Handler;
				try
				{
					scopeHandler.Disconnect(connection, this);
				}
				catch (Exception)
				{
				}
			}
			if (copyOnWriteArraySet.Count == 0)
			{
				_clients.Remove(client);
				if (scopeHandler != null)
				{
					try
					{
						scopeHandler.Leave(client, this);
					}
					catch (Exception)
					{
					}
				}
			}
			RemoveEventListener(connection);
		}
		if (base.HasParent)
		{
			Parent.Disconnect(connection);
		}
	}

	public bool HasChildScope(string name)
	{
		return _children.ContainsKey(ScopeType + Separator + name);
	}

	public bool HasChildScope(string type, string name)
	{
		return _children.ContainsKey(type + Separator + name);
	}

	public bool CreateChildScope(string name)
	{
		Scope scope = new Scope(name, _serviceContainer);
		scope.Parent = this;
		return AddChildScope(scope);
	}

	public bool AddChildScope(IBasicScope scope)
	{
		if (HasHandler && !Handler.AddChildScope(scope))
		{
			return false;
		}
		if (scope is IScope && HasHandler && !Handler.Start((IScope)scope))
		{
			return false;
		}
		_children[scope.Type + Separator + scope.Name] = scope;
		return true;
	}

	public void RemoveChildScope(IBasicScope scope)
	{
		if (scope is IScope && HasHandler)
		{
			Handler.Stop((IScope)scope);
		}
		string key = scope.Type + Separator + scope.Name;
		if (_children.ContainsKey(key))
		{
			_children.Remove(key);
		}
		if (HasHandler)
		{
			Handler.RemoveChildScope(scope);
		}
	}

	public ICollection GetScopeNames()
	{
		return _children.Keys;
	}

	public IEnumerator GetBasicScopeNames(string type)
	{
		if (type == null)
		{
			return _children.Keys.GetEnumerator();
		}
		return new PrefixFilteringStringEnumerator(_children.Keys, type + Separator);
	}

	public IBasicScope GetBasicScope(string type, string name)
	{
		string key = type + Separator + name;
		if (_children.ContainsKey(key))
		{
			return _children[key];
		}
		return null;
	}

	public IScope GetScope(string name)
	{
		string key = ScopeType + Separator + name;
		if (_children.ContainsKey(key))
		{
			return _children[key] as IScope;
		}
		return null;
	}

	public ICollection GetClients()
	{
		return _clients.Keys;
	}

	public IEnumerator GetConnections()
	{
		return new ConnectionIterator(this);
	}

	public IScopeContext GetContext()
	{
		if (!HasContext && base.HasParent)
		{
			return _parent.Context;
		}
		return _context;
	}

	public ICollection LookupConnections(IClient client)
	{
		if (_clients.ContainsKey(client))
		{
			return _clients[client];
		}
		return null;
	}

	public override IEnumerator GetEnumerator()
	{
		return _children.Values.GetEnumerator();
	}
}
