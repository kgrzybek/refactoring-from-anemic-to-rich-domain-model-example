using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public Employee ContactEmployee() => _employees.Single(x => x.Id == ContactEmployeeId);

        public Guid? ContactEmployeeId { get; set; }

        public string Source { get; set; }

        private List<Employee> _employees;

        private Company()
        {
            _employees = new List<Employee>();
        }

        private Company(string name, string source, ICompaniesCounter companiesCounter)
        {
            SetName(name, companiesCounter);
            
            this.Source = source;
        }

        public void AddEmployee(Guid personId, string firstName, string lastName,
            string phone, string email, bool isContactEmployee)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (_employees.Any(x =>  x.Email == email && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee email address must be unique in company");
                }
            }

            if (!string.IsNullOrEmpty(phone))
            {
                if (_employees.Any(x => x.Phone == phone && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee phone number must be unique in company");
                }
            }

            var employee = Employee.CreateNewEmployee(personId, this.Id, email, phone);

            if (isContactEmployee)
            {
                if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
                {
                    throw new BusinessException("Contact person must have e-mail or phone provided");
                }

                this.ContactEmployeeId = employee.Id;
            }
            
            _employees.Add(employee);
        }

        public void ChangeEmployeeContact(Guid employeeId, string phone, string email)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
            {
                if (this.ContactEmployeeId == employeeId)
                {
                    throw new BusinessException("Contact person must have e-mail or phone provided");
                }
            }

            if (!string.IsNullOrEmpty(email))
            {
                if (_employees.Any(x => x.Email == email && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee email address must be unique in company");
                }
            }

            if (!string.IsNullOrEmpty(phone))
            {
                if (_employees.Any(x =>  x.Phone == phone && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee phone number must be unique in company");
                }
            }

            var employee = _employees.Single(x => x.Id == employeeId);

            employee.ChangeContact(email, phone);
        }

        public static Company CreateEntered(string name, ICompaniesCounter companiesCounter)
        {
            return new Company(name, "Entered", companiesCounter);
        }

        public static Company CreateImported(string name, ICompaniesCounter companiesCounter)
        {   
            return new Company(name, "Imported", companiesCounter);
        }

        public void SetName(string name, ICompaniesCounter companiesCounter)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BusinessException("Company name must be provided");
            }

            if (companiesCounter.CountCompaniesByName(name) > 0)
            {
                throw new BusinessException("Company name must be unique");
            }

            if (this.Name == name)
            {
                return;
            }

            this.Name = name;
        }

        public void Deactivate(Guid employeeId)
        {
            var employee = _employees.SingleOrDefault(x => x.Id == employeeId && x.ActiveTo == null);

            if (employee == null)
            {
                throw new BusinessException("Employee cannot be deactivated more than once");
            }

            employee.Deactivate();                
        }
    }
}