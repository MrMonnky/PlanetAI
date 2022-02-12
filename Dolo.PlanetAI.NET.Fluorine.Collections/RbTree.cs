using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class RbTree
{
	public struct InsertResult
	{
		public bool NewNode;

		public RbTreeNode Node;

		public InsertResult(bool newNode, RbTreeNode node)
		{
			NewNode = newNode;
			Node = node;
		}
	}

	private RbTreeNode _root;

	private IComparer _comparer;

	public RbTreeNode Root => _root.Left;

	public IComparer Comparer => _comparer;

	public RbTreeNode First
	{
		get
		{
			RbTreeNode rbTreeNode = Root;
			while (rbTreeNode.Left != RbTreeNode.Nil)
			{
				rbTreeNode = rbTreeNode.Left;
			}
			return rbTreeNode;
		}
	}

	public RbTreeNode Last
	{
		get
		{
			RbTreeNode rbTreeNode = Root;
			while (rbTreeNode.Right != RbTreeNode.Nil)
			{
				rbTreeNode = rbTreeNode.Right;
			}
			return rbTreeNode;
		}
	}

	public RbTree(IComparer comparer)
	{
		_root = new RbTreeNode(null);
		_root.IsRed = false;
		_comparer = comparer;
	}

	private InsertResult BinaryInsert(RbTreeNode z, bool allowDuplicates, bool replaceIfDuplicate)
	{
		z.Left = RbTreeNode.Nil;
		z.Right = RbTreeNode.Nil;
		RbTreeNode rbTreeNode = _root;
		RbTreeNode rbTreeNode2 = _root.Left;
		while (rbTreeNode2 != RbTreeNode.Nil)
		{
			rbTreeNode = rbTreeNode2;
			int num = _comparer.Compare(rbTreeNode2.Value, z.Value);
			if (!allowDuplicates && num == 0)
			{
				if (replaceIfDuplicate)
				{
					rbTreeNode2.Value = z.Value;
				}
				return new InsertResult(newNode: false, rbTreeNode2);
			}
			rbTreeNode2 = ((num <= 0) ? rbTreeNode2.Right : rbTreeNode2.Left);
		}
		z.Parent = rbTreeNode;
		if (rbTreeNode == _root || _comparer.Compare(rbTreeNode.Value, z.Value) > 0)
		{
			rbTreeNode.Left = z;
		}
		else
		{
			rbTreeNode.Right = z;
		}
		z.Parent = rbTreeNode;
		return new InsertResult(newNode: true, z);
	}

	private void LeftRotate(RbTreeNode x)
	{
		RbTreeNode right = x.Right;
		x.Right = right.Left;
		if (right.Left != RbTreeNode.Nil)
		{
			right.Left.Parent = x;
		}
		right.Parent = x.Parent;
		if (x == x.Parent.Left)
		{
			x.Parent.Left = right;
		}
		else
		{
			x.Parent.Right = right;
		}
		right.Left = x;
		x.Parent = right;
	}

	private static void RightRotate(RbTreeNode y)
	{
		RbTreeNode left = y.Left;
		y.Left = left.Right;
		if (left.Right != RbTreeNode.Nil)
		{
			left.Right.Parent = y;
		}
		left.Parent = y.Parent;
		if (y == y.Parent.Left)
		{
			y.Parent.Left = left;
		}
		else
		{
			y.Parent.Right = left;
		}
		left.Right = y;
		y.Parent = left;
	}

	public InsertResult Insert(object val, bool allowDuplicates, bool replaceIfDuplicate)
	{
		RbTreeNode rbTreeNode = new RbTreeNode(val);
		InsertResult result = BinaryInsert(rbTreeNode, allowDuplicates, replaceIfDuplicate);
		if (!result.NewNode)
		{
			return result;
		}
		RbTreeNode rbTreeNode2 = rbTreeNode;
		rbTreeNode2.IsRed = true;
		while (rbTreeNode2.Parent.IsRed)
		{
			if (rbTreeNode2.Parent == rbTreeNode2.Parent.Parent.Left)
			{
				RbTreeNode right = rbTreeNode2.Parent.Parent.Right;
				if (right.IsRed)
				{
					rbTreeNode2.Parent.IsRed = false;
					right.IsRed = false;
					rbTreeNode2.Parent.Parent.IsRed = true;
					rbTreeNode2 = rbTreeNode2.Parent.Parent;
					continue;
				}
				if (rbTreeNode2 == rbTreeNode2.Parent.Right)
				{
					rbTreeNode2 = rbTreeNode2.Parent;
					LeftRotate(rbTreeNode2);
				}
				rbTreeNode2.Parent.IsRed = false;
				rbTreeNode2.Parent.Parent.IsRed = true;
				RightRotate(rbTreeNode2.Parent.Parent);
				continue;
			}
			RbTreeNode left = rbTreeNode2.Parent.Parent.Left;
			if (left.IsRed)
			{
				rbTreeNode2.Parent.IsRed = false;
				left.IsRed = false;
				rbTreeNode2.Parent.Parent.IsRed = true;
				rbTreeNode2 = rbTreeNode2.Parent.Parent;
				continue;
			}
			if (rbTreeNode2 == rbTreeNode2.Parent.Left)
			{
				rbTreeNode2 = rbTreeNode2.Parent;
				RightRotate(rbTreeNode2);
			}
			rbTreeNode2.Parent.IsRed = false;
			rbTreeNode2.Parent.Parent.IsRed = true;
			LeftRotate(rbTreeNode2.Parent.Parent);
		}
		_root.Left.IsRed = false;
		return new InsertResult(newNode: true, rbTreeNode);
	}

	private void DeleteFixUp(RbTreeNode x)
	{
		RbTreeNode left = _root.Left;
		while (!x.IsRed && left != x)
		{
			if (x == x.Parent.Left)
			{
				RbTreeNode right = x.Parent.Right;
				if (right.IsRed)
				{
					right.IsRed = false;
					x.Parent.IsRed = true;
					LeftRotate(x.Parent);
					right = x.Parent.Right;
				}
				if (!right.Right.IsRed && !right.Left.IsRed)
				{
					right.IsRed = true;
					x = x.Parent;
					continue;
				}
				if (!right.Right.IsRed)
				{
					right.Left.IsRed = false;
					right.IsRed = true;
					RightRotate(right);
					right = x.Parent.Right;
				}
				right.IsRed = x.Parent.IsRed;
				x.Parent.IsRed = false;
				right.Right.IsRed = false;
				LeftRotate(x.Parent);
				x = left;
				continue;
			}
			RbTreeNode left2 = x.Parent.Left;
			if (left2.IsRed)
			{
				left2.IsRed = false;
				x.Parent.IsRed = true;
				RightRotate(x.Parent);
				left2 = x.Parent.Left;
			}
			if (!left2.Right.IsRed && !left2.Left.IsRed)
			{
				left2.IsRed = true;
				x = x.Parent;
				continue;
			}
			if (!left2.Left.IsRed)
			{
				left2.Right.IsRed = false;
				left2.IsRed = true;
				LeftRotate(left2);
				left2 = x.Parent.Left;
			}
			left2.IsRed = x.Parent.IsRed;
			x.Parent.IsRed = false;
			left2.Left.IsRed = false;
			RightRotate(x.Parent);
			x = left;
		}
		x.IsRed = false;
	}

	public RbTreeNode Next(RbTreeNode x)
	{
		RbTreeNode root = _root;
		RbTreeNode rbTreeNode = x.Right;
		if (rbTreeNode != RbTreeNode.Nil)
		{
			while (rbTreeNode.Left != RbTreeNode.Nil)
			{
				rbTreeNode = rbTreeNode.Left;
			}
			return rbTreeNode;
		}
		rbTreeNode = x.Parent;
		while (x == rbTreeNode.Right)
		{
			x = rbTreeNode;
			rbTreeNode = rbTreeNode.Parent;
		}
		if (rbTreeNode == root)
		{
			return RbTreeNode.Nil;
		}
		return rbTreeNode;
	}

	public RbTreeNode Prev(RbTreeNode x)
	{
		RbTreeNode root = _root;
		RbTreeNode rbTreeNode = x.Left;
		if (rbTreeNode != RbTreeNode.Nil)
		{
			while (rbTreeNode.Right != RbTreeNode.Nil)
			{
				rbTreeNode = rbTreeNode.Right;
			}
			return rbTreeNode;
		}
		rbTreeNode = x.Parent;
		while (x == rbTreeNode.Left)
		{
			if (rbTreeNode == root)
			{
				return RbTreeNode.Nil;
			}
			x = rbTreeNode;
			rbTreeNode = rbTreeNode.Parent;
		}
		return rbTreeNode;
	}

	public RbTreeNode LowerBound(object val)
	{
		RbTreeNode rbTreeNode = Root;
		RbTreeNode result = RbTreeNode.Nil;
		while (rbTreeNode != RbTreeNode.Nil)
		{
			if (_comparer.Compare(val, rbTreeNode.Value) <= 0)
			{
				result = rbTreeNode;
				rbTreeNode = rbTreeNode.Left;
			}
			else
			{
				rbTreeNode = rbTreeNode.Right;
			}
		}
		return result;
	}

	public RbTreeNode UpperBound(object val)
	{
		RbTreeNode rbTreeNode = Root;
		RbTreeNode result = RbTreeNode.Nil;
		while (rbTreeNode != RbTreeNode.Nil)
		{
			if (_comparer.Compare(val, rbTreeNode.Value) < 0)
			{
				result = rbTreeNode;
				rbTreeNode = rbTreeNode.Left;
			}
			else
			{
				rbTreeNode = rbTreeNode.Right;
			}
		}
		return result;
	}

	public void Erase(RbTreeNode z)
	{
		RbTreeNode root = _root;
		RbTreeNode rbTreeNode = ((z.Left != RbTreeNode.Nil && z.Right != RbTreeNode.Nil) ? Next(z) : z);
		RbTreeNode rbTreeNode2 = ((rbTreeNode.Left == RbTreeNode.Nil) ? rbTreeNode.Right : rbTreeNode.Left);
		if (_root == (rbTreeNode2.Parent = rbTreeNode.Parent))
		{
			_root.Left = rbTreeNode2;
		}
		else if (rbTreeNode == rbTreeNode.Parent.Left)
		{
			rbTreeNode.Parent.Left = rbTreeNode2;
		}
		else
		{
			rbTreeNode.Parent.Right = rbTreeNode2;
		}
		if (!rbTreeNode.IsRed)
		{
			DeleteFixUp(rbTreeNode2);
		}
		if (rbTreeNode != z)
		{
			rbTreeNode.Left = z.Left;
			rbTreeNode.Right = z.Right;
			rbTreeNode.Parent = z.Parent;
			rbTreeNode.IsRed = z.IsRed;
			z.Left.Parent = (z.Right.Parent = rbTreeNode);
			if (z == z.Parent.Left)
			{
				z.Parent.Left = rbTreeNode;
			}
			else
			{
				z.Parent.Right = rbTreeNode;
			}
		}
		if (z.Value is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}

	public int Erase(object val)
	{
		RbTreeNode rbTreeNode = LowerBound(val);
		RbTreeNode rbTreeNode2 = UpperBound(val);
		int num = 0;
		while (rbTreeNode != rbTreeNode2)
		{
			RbTreeNode z = rbTreeNode;
			rbTreeNode = Next(rbTreeNode);
			Erase(z);
			num++;
		}
		return num;
	}
}
