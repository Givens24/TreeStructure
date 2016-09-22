using System;
using System.Collections.Generic;
using System.Linq;
using Tree.Structures;

namespace Tree.Extensions
{
    public static class TreeExtensions
    {

        /// <summary>
        /// Traverses down the tree structure starting at a specific leaf and performs a specific action on that leaf
        /// </summary>
        /// <typeparam name="T">The type for the tree/leaf structure</typeparam>
        /// <param name="leaf">The leaf to start searching on</param>
        /// <param name="action">The action to perform against the leaf</param>
        public static void TraverseTree<T>(this Leaf<T> leaf, Action<Leaf<T>> action) where T : class
        {
            leaf.Children.ToList().ForEach(x =>
            {
                action(x);
                TraverseTree(x, action);
            });
        }

        /// <summary>
        /// Finds leaves in the tree based on a specific where clause starting at a specific leaf
        /// </summary>
        /// <typeparam name="T">The type for the tree/leaf structure</typeparam>
        /// <param name="leaf">The leaf to start searching on</param>
        /// <param name="whereExpression">The where clause condition</param>
        /// <returns>A collection of leaves</returns>
        public static IEnumerable<Leaf<T>> FindLeaves<T>(this Leaf<T> leaf, Func<Leaf<T>, bool> whereExpression) where T : class
        {
            var flattenedLeaves = new List<Leaf<T>>();
            leaf.TraverseTree(x =>
            {
                flattenedLeaves.Add(x);
            });

            return flattenedLeaves.Where(whereExpression);
        }

        /// <summary>
        /// Flattens the tree structure into a collection of leaves
        /// </summary>
        /// <typeparam name="T">The type for the tree/leaf structure</typeparam>
        /// <param name="tree">The tree structure to flatten</param>
        /// <returns>A collection of leaves</returns>
        public static IEnumerable<Leaf<T>> FlattenTree<T>(this Tree<T> tree) where T : class
        {
            var leaves = new List<Leaf<T>> {tree.Root};
            tree.Root.TraverseTree<T>(x =>
            {
                leaves.Add(x);
            });

            return leaves;
        }

        /// <summary>
        /// Finds the root of the tree starting at a specific leaf
        /// </summary>
        /// <typeparam name="T">The type for the tree/leaf structure</typeparam>
        /// <param name="leaf">The leaf to start searching on</param>
        /// <returns>The root leaf in the tree</returns>
        public static Leaf<T> FindRoot<T>(this Leaf<T> leaf) where T : class
        {
            if (leaf.Parent == null)
            {
                return leaf;
            }

            return FindRoot(leaf.Parent);
        }
    }
}
