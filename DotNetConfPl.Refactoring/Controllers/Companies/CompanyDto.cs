using System;

namespace DotNetConfPl.Refactoring.Controllers.Companies
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContactEmployeeFullName { get; set; }

        public string ContactEmployeeEmail { get; set; }

        public string ContactEmployeePhone { get; set; }

        public string Source { get; set; }
    }
}