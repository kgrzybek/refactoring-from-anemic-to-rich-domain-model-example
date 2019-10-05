using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Employee ContactEmployee { get; set; }

        public Guid? ContactEmployeeId { get; set; }

        public string Source { get; set; }
    }
}