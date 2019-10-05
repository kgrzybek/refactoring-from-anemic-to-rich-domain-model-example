using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Person
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string FullName { get; private set; }

        private Person()
        {

        }

        private Person(string firstName, string lastName)
        {
            this.Id = Guid.NewGuid();

            this.SetPersonNames(firstName, lastName);
        }

        public static Person CreateEmployeePerson(string firstName, string lastName)
        {
            return new Person(firstName, lastName);
        }

        public void SetPersonNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                throw new BusinessException("Person must have defined first or last name");
            }

            this.FirstName = firstName;
            this.LastName = lastName;

            if (string.IsNullOrEmpty(firstName))
            {
                this.FullName = lastName;
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                this.FullName = firstName;
            }
            else
            {
                this.FullName = $"{firstName} {lastName}";
            }
        }
    }
}