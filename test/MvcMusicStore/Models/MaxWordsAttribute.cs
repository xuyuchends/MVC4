using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MvcMusicStore.Models
{
    public class MaxWordsAttribute:ValidationAttribute
    {
        private readonly int _maxWords;
        public MaxWordsAttribute(int maxWords):base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueString = value.ToString();
                if (valueString.Split(' ').Length > 3)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}