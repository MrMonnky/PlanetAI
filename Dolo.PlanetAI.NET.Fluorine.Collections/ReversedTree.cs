using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal sealed class ReversedTree : IEnumerable
{
	private sealed class Enumerator : IEnumerator
	{
		private RbTree _tree;

		private RbTreeNode _currentNode;

		public object Current => _currentNode.Value;

		public Enumerator(RbTree tree)
		{
			_tree = tree;
		}

		public void Reset()
		{
			_currentNode = null;
		}

		public bool MoveNext()
		{
			if (_currentNode == null)
			{
				_currentNode = _tree.First;
			}
			else
			{
				_currentNode = _tree.Next(_currentNode);
			}
			return !_currentNode.IsNull;
		}
	}

	private RbTree _tree;

	public ReversedTree(RbTree tree)
	{
		_tree = tree;
	}

	public IEnumerator GetEnumerator()
	{
		return new Enumerator(_tree);
	}
}
