using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public Guid CompanyId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }

        private Employee()
        {

        }

        private Employee(Guid personId, Guid companyId, string email, string phone)
        {
            this.Id = Guid.NewGuid();
            this.CompanyId = companyId;
            this.PersonId = personId;
            this.Email = email;
            this.Phone = phone;
            this.ActiveFrom = DateTime.UtcNow;
        }

        internal static Employee CreateNewEmployee(Guid personId, Guid companyId, string email, string phone)
        {
            return new Employee(personId, companyId, email, phone);
        }

        internal void ChangeContact(string email, string phone)
        {
            this.Email = email;
            this.Phone = phone;
        }
    }
}