namespace Zad3
{
    public class InnerNode : Node
    {
        #region Properties

        public int Key { get; private set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        #endregion
        #region Ctors

        public InnerNode(int key)
        {
            Key = key;
            Left = new LeafNode();
            Right = new LeafNode();
        }

        #endregion
        #region Overrides

        public override void Accept(TreeVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}