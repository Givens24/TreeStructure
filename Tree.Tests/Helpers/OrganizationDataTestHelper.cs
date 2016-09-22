using System;
using System.Collections.Generic;
using Tree.Tests.TestModels;
using Tree.Structures;

namespace Tree.Tests.Helpers
{
    public class OrganizationDataTestHelper
    {
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

        private Organization GetOrganizationData()
        {
            var organization = new Organization
            {
                Id = Guid.NewGuid(),
                BusinessType = "IT",
                Name = "Test Organization",
                Address = "123 Happy Lane",
                State = "MN",
                ZipCode = 55124,
                SubOrganizations = new List<Organization>
                {
                    new Organization
                    {
                        Id = Guid.NewGuid(),
                        BusinessType = "IT",
                        Name = "Test Sub Organization",
                        Address = "456 Sad Street",
                        State = "MN",
                        ZipCode = 55337,
                    },
                    new Organization
                    {
                        Id = Guid.NewGuid(),
                        BusinessType = "IT",
                        Name = "Test Sub Organization 1",
                        Address = "678 Tough Street",
                        State = "MN",
                        ZipCode = 55447,
                        SubOrganizations = new List<Organization>
                        {
                            new Organization
                            {
                                Id = Guid.NewGuid(),
                                BusinessType = "IT",
                                Name = "Sub Organization 1 - Dev Branch",
                                Address = "678 Tough Street",
                                State = "MN",
                                ZipCode = 55447,
                            },
                            new Organization
                            {
                                Id = Guid.NewGuid(),
                                BusinessType = "IT",
                                Name = "Sub Organization 1 - Ops Branch",
                                Address = "678 Tough Street",
                                State = "MN",
                                ZipCode = 55447,
                            },
                        }
                    },
                    new Organization
                    {
                        Id = Guid.NewGuid(),
                        BusinessType = "IT",
                        Name = "Test Sub Organization 2",
                        Address = "87564 Far Away Drive",
                        State = "MN",
                        ZipCode = 55044,
                        SubOrganizations = new List<Organization>
                        {
                            new Organization
                            {
                                Id = Guid.NewGuid(),
                                BusinessType = "IT",
                                Name = "Sub Organization 2 - Dev Branch",
                                Address = "87564 Far Away Drive",
                                State = "MN",
                                ZipCode = 55044,
                            },
                            new Organization
                            {
                                Id = Guid.NewGuid(),
                                BusinessType = "IT",
                                Name = "Sub Organization 2 - Ops Branch",
                                Address = "87564 Far Away Drive",
                                State = "MN",
                                ZipCode = 55044,
                            }
                        }
                    },
                }
            };

            return organization;
        }
    }
}
