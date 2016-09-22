# Tree Structure
A generic tree/leaf structure built with extensions used to traverse data easily from any point in the structure.
* This library can be found on **NuGet**
* The tree structure is built from a ```Leaf<T>``` that represents the starting point or **"Root"** of
the tree.
* Each leaf has a **LeafData** property which represents a single instance of **T** (The data type your tree structure is made of.).
* Each leaf also has a **Children** property which is an ```IEnumerable<Leaf<T>>``` that are nested collections on your leaf.
* Each leaf also has a **Parent** property which is a Leaf<T> that points to the Leaf directly above the leaf that your are on. (**NOTE**: The Parent is null if you don't set it. If the parents aren't set, it would prevent you from walking up the tree's parent leaves.)

***

# Setup and Usage of the Tree Structure
* The following examples will show you how to setup a tree and use the extensions necessary to locate the data within the structure.

* The following example demonstrates how to build a complete tree structure using recursion.
* Assuming that we have an **Organization** class with a property called **SubOrganizations** which is an ```IEnumerable<Organization>```, the methods below setup the root organization along with all of the nested organizations.
```C#
        protected Tree<Organization> BuildOrganizationTree()
        {
            var organizationData = GetOrganizationData();
            var organizationTree = new Tree<Organization>(organizationData);
            AddOrganization(organizationData, organizationTree.Root);

            return organizationTree;
        }

        private void AddOrganization(Organization organization, Leaf<Organization> leaf)
        {
            if (organization.SubOrganizations != null)
            {
                foreach (var subOrganization in organization.SubOrganizations)
                {
                    var newLeafForSubOrganization = new Leaf<Organization>(subOrganization);
                    newLeafForSubOrganization.SetParent(leaf);
                    leaf.AddChildLeaf(newLeafForSubOrganization);
                    AddOrganization(subOrganization, newLeafForSubOrganization);
                }
            }
        }
```
* Once the tree is built you can use the following extensions to traverse the tree structure in order to locate the desired data.
* The Tree solution has a folder of **"Extensions"** that include **"TraverseTree"**, **"FindLeaves"**, **"FlattenTree"** and **"FindRoot"**.
* **TraverseTree** will extend on any leaf, search down the rest of the children and execute the action parameter against the leaves.
```C#
           _organizationTree.Root.TraverseTree(x =>
           {
               if (x.LeafData.ZipCode.Equals(55447))
               {
                   x.LeafData.ZipCode = 55445;
               }
           });
```

* **FindLeaves** will extend on any leaf and return an ```IEnumerable<Leaf<T>>``` that is comprised of the data filtered by the where clause parameter that is passed into the function. The whereExpression parameter is a ```Func<Leaf<T>, bool>```
```C#
var organizations = _organizationTree.Root.FindLeaves(x => x.LeafData.ZipCode.Equals(55447));
```

* **FlatenTree** is an extension of the ```Tree<T>``` and will return an ```IEnumerable<Leaf<T>>``` that has every leaf in the tree laid out into one single collection.
```C#
var flattenedTree = _organizationTree.FlattenTree();
```

* **FindRoot** will extend on any leaf and walk up the tree's parents in order to find the **"Root"** of the tree. (**NOTE**: If you do not call The SetParent() function on the leaves as you are building the tree, this function will not work as intended. A complete tree structure is needed for it to traverse properly.) The following example finds a specific
leaf and finds the root from that point in the tree structure.
```C#
var organization = _organizationTree.Root.FindLeaves(x => x.LeafData.Name.Equals("Sub Organization 1 - Dev Branch")).FirstOrDefault();

var root = organization.FindRoot();
```

