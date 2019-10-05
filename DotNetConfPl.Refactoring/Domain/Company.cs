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

        private Company()
        {
            
        }

        private Company(string name, string source)
        {
            SetName(name);
            
            this.Source = source;
        }

        public static Company CreateEntered(string name)
        {
            return new Company(name, "Entered");
        }

        public static Company CreateImported(string name)
        {
            
            return new Company(name, "Imported");
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BusinessException("Company name must be provided");
            }

            this.Name = name;
        }
    }
}