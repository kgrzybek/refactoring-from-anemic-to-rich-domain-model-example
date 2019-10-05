using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public Employee ContactEmployee { get; set; }

        public Guid? ContactEmployeeId { get; set; }

        public string Source { get; set; }

        private Company()
        {
            
        }

        private Company(string name, string source, ICompaniesCounter companiesCounter)
        {
            SetName(name, companiesCounter);
            
            this.Source = source;
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
    }
}