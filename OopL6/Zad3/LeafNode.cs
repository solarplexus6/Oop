namespace Zad3
{
    public class LeafNode : Node
    {
        #region Overrides

        public override void Accept(TreeVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}