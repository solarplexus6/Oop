using System;
using System.Diagnostics;

namespace Zad3
{
    public class HeightVisitor : TreeVisitor
    {
        #region Private fields

        private Node _root;

        #endregion
        #region Properties

        public int TreeHeight
        {
            get
            {
                if (_root == null)
                {
                    throw new InvalidOperationException();
                }
                if (_root is LeafNode)
                {
                    return 0;
                }
                return GetHeight(_root);
            }
        }

        #endregion
        #region Overrides

        public override void Visit(LeafNode node)
        {
            _root = node;
        }

        public override void Visit(InnerNode node)
        {
            _root = node;
        }

        #endregion
        #region Private methods

        private int GetHeight(Node node)
        {
            if (node is LeafNode)
            {
                return -1;
            }
            var inner = node as InnerNode;
            Debug.Assert(inner != null, "inner != null");
            return Math.Max(GetHeight(inner.Left), GetHeight(inner.Right)) + 1;
        }

        #endregion
    }
}