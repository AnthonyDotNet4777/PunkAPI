using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PunkAPI.DataTransferObject
{
    public class BeerReviewObject
    {
        [ValidateEmail]
        public String Username { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comments { get; set; }
    }

    public class ValidateEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            var review = (BeerReviewObject)validationContext.ObjectInstance;

            if (review.Username == null)
            {
                return new ValidationResult("Username should not be null");
            }

            if (!regex.IsMatch(review.Username))
            {
                return new ValidationResult("Please enter valid email address");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}