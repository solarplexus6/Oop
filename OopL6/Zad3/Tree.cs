using System.Diagnostics;

namespace Zad3
{
    public class Tree
    {
        #region Private fields

        private Node _root = new LeafNode();

        #endregion
        #region Public methods

        public void Accept(TreeVisitor visitor)
        {
            _root.Accept(visitor);
        }

        public void Add(int e)
        {
            if (_root is LeafNode)
            {
                _root = new InnerNode(e);
            }
            else
            {
                var current = _root;
                InnerNode prev = null;
                while (current is InnerNode)
                {
                    prev = (InnerNode) current;
                    var inner = current as InnerNode;
                    current = inner.Key > e ? inner.Right : inner.Left;
                }

                Debug.Assert(prev != null, "prev != null");
                if (prev.Key > e)
                {
                    prev.Right = new InnerNode(e);
                }
                else
                {
                    prev.Left = new InnerNode(e);
                }
            }
        }

        #endregion
    }
}