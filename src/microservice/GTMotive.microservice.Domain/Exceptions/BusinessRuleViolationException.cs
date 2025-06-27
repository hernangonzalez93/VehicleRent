using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Domain.Exceptions
{
    /// <summary>
    /// BusinessRuleViolationException is thrown when a business rule is violated.
    /// </summary>
    public class BusinessRuleViolationException : Exception
    {
        /// <summary>
        /// BusinessRuleViolationException constructor with a message parameter.
        /// </summary>
        /// <param name="message"></param>
        public BusinessRuleViolationException(string message) : base(message) { }
    }
}
