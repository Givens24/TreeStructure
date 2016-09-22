using System;
using System.Collections.Generic;

namespace Tree.Tests.TestModels
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BusinessType { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public long ZipCode { get; set; }
        public IEnumerable<Organization> SubOrganizations { get; set; }
    }
}
