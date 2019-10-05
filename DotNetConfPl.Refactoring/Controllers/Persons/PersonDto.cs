using System;

namespace DotNetConfPl.Refactoring.Controllers.Persons
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}