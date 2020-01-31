using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WonderAPI.Pkg
{
    public class ModelValidator
    {
        public static void Validate(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationResultIsValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
            if (!validationResultIsValid)
            {
                ValidationResult validationResult = new ValidationResult(""); 
                foreach (var item in validationResults)
                {
                    FieldError fieldError = new FieldError
                    {
                        Field = item.MemberNames.First(),
                        ErrorMessage = item.ErrorMessage
                    };
                    validationResult.Errors.Add(fieldError);
                }

                throw new ValidationException(validationResult); 
            }
        }
    }
}
