using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }
    }
}