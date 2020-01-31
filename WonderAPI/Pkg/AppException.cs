using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// Represent error per field.
    /// </summary>
    public class FieldError
    {
        public string Field { get; set; }
        public string ErrorMessage { get; set; }
        public List<FieldError> SubErrors { get; set; } = new List<FieldError>(); 
    }

    /// <summary>
    /// Represent validation result, containing list of errors. When model validation failed, server will response with this model.
    /// </summary>
    public class ValidationResult : FieldError
    {
        public List<FieldError> Errors { get; set; } = new List<FieldError>();
    }

    /// <summary>
    /// Represent the known error/exception happened in this application
    /// </summary>
    public class AppException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public AppException()
        {
        }

        public AppException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
