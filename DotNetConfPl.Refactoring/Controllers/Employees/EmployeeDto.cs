using System;

namespace DotNetConfPl.Refactoring.Controllers.Employees
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }

        public Guid PersonId { get; set; }

        public bool IsContact { get; set; }
    }
}