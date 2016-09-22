using System.Collections.Generic;

namespace Tree.Structures
{
    public class Leaf<T> where T : class
    {
        public T LeafData { get; }
        public Leaf<T> Parent => _parent;
        private Leaf<T> _parent;
        public IEnumerable<Leaf<T>> Children => _children;
        private readonly List<Leaf<T>> _children = new List<Leaf<T>>();

        public Leaf(T leafData)
        {
            LeafData = leafData;
        }

        public void SetParent(Leaf<T> parentLeaf)
        {
            if (_parent == null)
            {
                _parent = parentLeaf;
            }
        }

        public void AddChildLeaf(Leaf<T> childLeaf)
        {
            _children.Add(childLeaf);
        }
    }
}
