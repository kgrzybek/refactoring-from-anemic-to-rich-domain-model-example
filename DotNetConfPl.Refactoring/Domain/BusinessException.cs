using System;

namespace DotNetConfPl.Refactoring.Domain
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
            
        }
    }
}