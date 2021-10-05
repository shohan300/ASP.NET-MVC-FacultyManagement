using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyManagement.Repositories
{
    public class JoinDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentDate = DateTime.Now;
            string message = string.Empty;
            if (Convert.ToDateTime(value)>=currentDate)
            {
                message = "Join Date Must Match The Current Date Or Less";
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}