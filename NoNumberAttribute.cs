using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Validations
{
    public class NoNumberAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && Regex.IsMatch(value.ToString()!, @"\d"))
            {
                return new ValidationResult("Numbers are not allowed in this field.");
            }
            return ValidationResult.Success!;
        
            }
    }
}
