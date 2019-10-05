namespace DotNetConfPl.Refactoring.Controllers.Employees
{
    public class AddEmployeeRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsContactEmployee { get; set; }
    }
}