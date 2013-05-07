namespace Zad3
{
    public abstract class TreeVisitor
    {
        #region Public methods

        public abstract void Visit(LeafNode node);
        public abstract void Visit(InnerNode node);

        #endregion
    }
}