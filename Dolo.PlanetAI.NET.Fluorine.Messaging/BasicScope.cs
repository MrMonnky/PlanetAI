using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class BasicScope : PersistableAttributeStore, IBasicScope, ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable
{
	private object _syncLock = new object();

	protected IScope _parent;

	protected CopyOnWriteArray<IEventListener> _listeners = new CopyOnWriteArray<IEventListener>();

	protected bool _keepOnDisconnect = false;

	public object SyncRoot => _syncLock;

	public bool HasParent => _parent != null;

	public virtual IScope Parent
	{
		get
		{
			return _parent;
		}
		set
		{
			_parent = value;
		}
	}

	public int Depth
	{
		get
		{
			if (HasParent)
			{
				return _parent.Depth + 1;
			}
			return 0;
		}
	}

	public override string Path
	{
		get
		{
			if (HasParent)
			{
				return _parent.Path + "/" + _parent.Name;
			}
			return string.Empty;
		}
	}

	public BasicScope(IScope parent, string type, string name, bool persistent)
		: base(type, name, null, persistent)
	{
		_parent = parent;
	}

	public virtual void AddEventListener(IEventListener listener)
	{
		_listeners.Add(listener);
	}

	public virtual void RemoveEventListener(IEventListener listener)
	{
		_listeners.Remove(listener);
		if (!_keepOnDisconnect && _listeners.Count == 0)
		{
			_parent.RemoveChildScope(this);
		}
	}

	public ICollection GetEventListeners()
	{
		return _listeners;
	}

	public bool HandleEvent(IEvent evt)
	{
		return false;
	}

	public void NotifyEvent(IEvent evt)
	{
	}

	public virtual void DispatchEvent(IEvent evt)
	{
		foreach (IEventListener listener in _listeners)
		{
			if (evt.Source == null || evt.Source != listener)
			{
				listener.NotifyEvent(evt);
			}
		}
	}

	public virtual IEnumerator GetEnumerator()
	{
		return null;
	}

	public override string ToString()
	{
		return Name;
	}
}
