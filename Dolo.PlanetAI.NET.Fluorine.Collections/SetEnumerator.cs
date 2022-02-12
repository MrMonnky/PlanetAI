using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class SetEnumerator : IEnumerator
{
	private RbTree _tree;

	private RbTreeNode _currentNode = null;

	public object Current => _currentNode.Value;

	public SetEnumerator(RbTree tree)
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
