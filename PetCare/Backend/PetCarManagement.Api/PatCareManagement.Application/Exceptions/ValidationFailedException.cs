using System;

using System;
using System.Collections.Generic;
namespace PetCareManagement.Application.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationFailedException(IDictionary<string, string[]> errors)
            : base("Validation failed for one or more entities.")
        {
            Errors = errors;
        }
    }
}