using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RECOApp.Models
{
    public class PageNumberAttribute : ValidationAttribute
    {
        private int _number;

        public PageNumberAttribute(int Number)
        {
            _number = Number;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Page page = (Page)validationContext.ObjectInstance;

            if (movie.Genre == Genre.Classic && movie.ReleaseDate.Year > _year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
