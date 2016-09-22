using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tree.Tests.TestModels;
using Tree.Extensions;
using Tree.Structures;
using Tree.Tests.Helpers;

namespace Tree.Tests.Extensions
{
    [TestClass]
    public class TreeExtensionsTests : OrganizationDataTestHelper
    {
        private Tree<Organization> _organizationTree;
        [TestInitialize]
        public void Setup()
        {
            _organizationTree = BuildOrganizationTree();
        }

        [TestMethod]
        public void TraverseTree_Successfully_Change_Zip_Code_On_Organization()
        {
           _organizationTree.Root.TraverseTree(x =>
           {
               if (x.LeafData.ZipCode.Equals(55447))
               {
                   x.LeafData.ZipCode = 55445;
               }
           });

            Assert.IsFalse(_organizationTree.Root.FindLeaves(x => x.LeafData.ZipCode.Equals(55447)).Any());
        }

        [TestMethod]
        public void FindLeaves_Successfully_Find_Leaves_With_Specific_Organization_Data()
        {
            var organizations = _organizationTree.Root.FindLeaves(x => x.LeafData.ZipCode.Equals(55447));

            Assert.IsTrue(organizations.Count() == 3);
        }

        [TestMethod]
        public void FindLeaves_No_Leaves_Found_With_Specific_Organization_Data()
        {
            var organizations = _organizationTree.Root.FindLeaves(x => x.LeafData.ZipCode.Equals(99874));

            Assert.IsFalse(organizations.Any());
        }

        [TestMethod]
        public void FindRoot_Successfully_Find_Root_Starting_At_A_Specific_Leaf()
        {
            var organization = _organizationTree.Root.FindLeaves(x => x.LeafData.Name.Equals("Sub Organization 1 - Dev Branch")).FirstOrDefault();
            var root = organization.FindRoot();

            Assert.IsTrue(root.LeafData.Name.Equals("Test Organization"));
        }

        [TestMethod]
        public void FlattenTree_Successfully_Flatten_Tree()
        {
            var flattenedTree = _organizationTree.FlattenTree();

            Assert.IsTrue(flattenedTree.Count() == 8);
        }
    }
}
