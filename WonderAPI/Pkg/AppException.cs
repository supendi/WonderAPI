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
    public class ValidationResult
    {
        public string Message { get; set; }
        public List<FieldError> Errors { get; set; } = new List<FieldError>();
        public ValidationResult(string errorMessage)
        {
            Message = errorMessage;
        }
    }

    /// <summary>
    /// Represent the known error/exception happened in this application
    /// </summary>
    public class AppException : Exception
    {

        public AppException()
        {
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

    public class ValidationException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
