using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Csharp_belt.Extensions
{
    public class AgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please enter a date");
            }
            else if ((((DateTime)value).Year) > (DateTime.Now.Year - 18))
            {
                return new ValidationResult("Must be 18+ years old.");
            }
            return ValidationResult.Success;
        }
    }

    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please enter a date");
            }
            else if ((DateTime)value < DateTime.Now.Date)
            {
                return new ValidationResult("Must be a future date");
            }
            return ValidationResult.Success;
        }
    }

    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please enter a password");
            }
            else
            {
                Regex PassLowerVal = new Regex("^(?=.*[a-z])$");
                Regex PassUppererVal = new Regex("^(?=.*[A-Z])$");
                Regex PassNumVal = new Regex("^(?=.*[0-9])$");
                Regex PassSpecialVal = new Regex("^(?=.*?[#?!@$%^&*-])$");
                MatchCollection match1 = PassLowerVal.Matches((string)value);
                MatchCollection match2 = PassUppererVal.Matches((string)value);
                MatchCollection match3 = PassNumVal.Matches((string)value);
                MatchCollection match4 = PassSpecialVal.Matches((string)value);
                if (match1.Count < 1)
                {
                    return new ValidationResult("Password must include a lowercase character");
                }
                else if (match2.Count < 1)
                {
                    return new ValidationResult("Password must include an upperercase character");
                }
                else if (match3.Count < 1)
                {
                    return new ValidationResult("Password must include a number");
                }
                else if (match4.Count < 1)
                {
                    return new ValidationResult("Password must include a special character");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}
