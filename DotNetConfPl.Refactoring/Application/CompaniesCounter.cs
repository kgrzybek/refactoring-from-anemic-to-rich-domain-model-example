using System.Linq;
using DotNetConfPl.Refactoring.Domain;
using DotNetConfPl.Refactoring.Infrastructure;

namespace DotNetConfPl.Refactoring.Application
{
    public class CompaniesCounter : ICompaniesCounter
    {
        private readonly CompaniesContext _companiesContext;

        public CompaniesCounter(CompaniesContext companiesContext)
        {
            _companiesContext = companiesContext;
        }

        public int CountCompaniesByName(string name)
        {
            return _companiesContext.Companies.Count(x => x.Name == name);
        }
    }
}