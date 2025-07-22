using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Funkcia validuje objekt.
        /// Vracia isValid a modelState.
        /// </summary>
        public static (bool IsValid, ModelStateDictionary? modelState) ValidateAndHandleModelState<T>(
            T obj,
            ModelStateDictionary modelState)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                obj,
                context,
                results,
                validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        modelState.AddModelError(memberName, validationResult.ErrorMessage ?? "Validation error");
                    }
                }

                return (false, modelState);
            }

            return (true, null);
        }
    }
}