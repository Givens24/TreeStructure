
namespace Tree.Structures
{
    public class Tree<T> where T : class 
    {
        public Leaf<T> Root { get; }
        public Tree(T root)
        {
            Root = new Leaf<T>(root);
        }
    }
}
