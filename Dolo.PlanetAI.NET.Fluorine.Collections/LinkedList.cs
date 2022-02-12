using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

[Serializable]
internal class LinkedList : IList, ICollection, IEnumerable
{
	[Serializable]
	private class Node
	{
		private object _value;

		private Node _next;

		private Node _previous;

		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		public Node NextNode
		{
			get
			{
				return _next;
			}
			set
			{
				_next = value;
			}
		}

		public Node PreviousNode
		{
			get
			{
				return _previous;
			}
			set
			{
				_previous = value;
			}
		}

		public Node(object val, Node previous, Node next)
		{
			_value = val;
			_next = next;
			_previous = previous;
		}
	}

	private class NodeHolder
	{
		private int _index;

		private Node _node;

		public int Index => _index;

		public Node Node => _node;

		public NodeHolder(Node node, int index)
		{
			_node = node;
			_index = index;
		}
	}

	private class LinkedListEnumerator : IEnumerator
	{
		private LinkedList _ll;

		private Node _current;

		private int _modId;

		public object Current => _current.Value;

		public LinkedListEnumerator(LinkedList ll)
		{
			_ll = ll;
			_modId = ll._modId;
			_current = _ll._rootNode;
		}

		public bool MoveNext()
		{
			if (_modId != _ll._modId)
			{
				throw new InvalidOperationException("LinkedList has been modified.");
			}
			_current = _current.NextNode;
			return (_current != _ll._rootNode) ? true : false;
		}

		public void Reset()
		{
			_current = _ll._rootNode;
		}
	}

	private int _nodeIndex;

	private Node _rootNode;

	private int _modId;

	public bool IsReadOnly => false;

	public object this[int index]
	{
		get
		{
			return GetNode(index).Value;
		}
		set
		{
			GetNode(index).Value = value;
		}
	}

	public bool IsFixedSize => false;

	public bool IsSynchronized => false;

	public int Count => _nodeIndex;

	public object SyncRoot => this;

	public LinkedList()
	{
		_rootNode = new Node(null, null, null);
		_rootNode.PreviousNode = _rootNode;
		_rootNode.NextNode = _rootNode;
	}

	public LinkedList(IList list)
		: this()
	{
		AddAll(list);
	}

	public void RemoveAt(int index)
	{
		CheckUpdateState();
		RemoveNode(GetNode(index));
	}

	public void Insert(int index, object value)
	{
		CheckUpdateState();
		Node node = null;
		if (index == _nodeIndex)
		{
			node = new Node(value, _rootNode.PreviousNode, _rootNode);
		}
		else
		{
			Node node2 = GetNode(index);
			node = new Node(value, node2.PreviousNode, node2);
		}
		node.PreviousNode.NextNode = node;
		node.NextNode.PreviousNode = node;
		_nodeIndex++;
		_modId++;
	}

	public void Remove(object value)
	{
		CheckUpdateState();
		NodeHolder node = GetNode(value);
		RemoveNode(node.Node);
	}

	public bool Contains(object value)
	{
		return GetNode(value) != null;
	}

	public void Clear()
	{
		_rootNode = new Node(null, null, null);
		_rootNode.PreviousNode = _rootNode;
		_rootNode.NextNode = _rootNode;
		_nodeIndex = 0;
		_modId++;
	}

	public int IndexOf(object value)
	{
		return GetNode(value)?.Index ?? (-1);
	}

	public int Add(object value)
	{
		Insert(_nodeIndex, value);
		return _nodeIndex - 1;
	}

	public void AddAll(IList elements)
	{
		foreach (object element in elements)
		{
			Add(element);
		}
	}

	private void CheckUpdateState()
	{
		if (IsReadOnly || IsFixedSize)
		{
			throw new NotSupportedException("LinkedList cannot be modified.");
		}
	}

	private void ValidateIndex(int index)
	{
		if (index < 0 || index >= _nodeIndex)
		{
			throw new ArgumentOutOfRangeException("index");
		}
	}

	private Node GetNode(int index)
	{
		ValidateIndex(index);
		Node node = _rootNode;
		for (int i = 0; i <= index; i++)
		{
			node = node.NextNode;
		}
		return node;
	}

	private NodeHolder GetNode(object value)
	{
		int num = 0;
		if (value == null)
		{
			for (Node nextNode = _rootNode.NextNode; nextNode != _rootNode; nextNode = nextNode.NextNode)
			{
				if (nextNode.Value == null)
				{
					return new NodeHolder(nextNode, num);
				}
				num++;
			}
		}
		else
		{
			for (Node nextNode2 = _rootNode.NextNode; nextNode2 != _rootNode; nextNode2 = nextNode2.NextNode)
			{
				if (value.Equals(nextNode2.Value))
				{
					return new NodeHolder(nextNode2, num);
				}
				num++;
			}
		}
		return null;
	}

	private void RemoveNode(Node node)
	{
		Node previousNode = node.PreviousNode;
		previousNode.NextNode = node.NextNode;
		node.NextNode.PreviousNode = previousNode;
		node.PreviousNode = null;
		node.NextNode = null;
		_nodeIndex--;
		_modId++;
	}

	public void CopyTo(Array array, int index)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (index < 0 || index > array.Length)
		{
			throw new ArgumentOutOfRangeException("index", $"Index {index} is out of range.");
		}
		if (array.Length - index < _nodeIndex)
		{
			throw new ArgumentException("Array is of insufficient size.");
		}
		Node node = _rootNode;
		int num = 0;
		int num2 = index;
		while (num < _nodeIndex)
		{
			node = node.NextNode;
			array.SetValue(node.Value, num2);
			num++;
			num2++;
		}
	}

	public IEnumerator GetEnumerator()
	{
		return new LinkedListEnumerator(this);
	}
}
