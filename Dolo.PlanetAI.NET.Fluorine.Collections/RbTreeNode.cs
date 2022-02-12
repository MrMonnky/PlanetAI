namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class RbTreeNode
{
	private class NullNode : RbTreeNode
	{
		public override bool IsNull => true;

		public NullNode()
		{
			Parent = this;
			Left = this;
			Right = this;
			IsRed = false;
		}
	}

	public object Value;

	public RbTreeNode Parent;

	public RbTreeNode Left;

	public RbTreeNode Right;

	public bool IsRed;

	public static readonly RbTreeNode Nil = new NullNode();

	public virtual bool IsNull => false;

	public RbTreeNode(object val)
		: this(val, Nil, Nil, Nil, isRed: true)
	{
	}

	public RbTreeNode(object val, RbTreeNode parent, RbTreeNode left, RbTreeNode right, bool isRed)
	{
		Value = val;
		Parent = parent;
		Left = left;
		Right = right;
		IsRed = isRed;
	}

	protected RbTreeNode()
	{
	}
}
