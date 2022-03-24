using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessageFormatString = "El campo {0} es requerido.";
        private readonly string Properties;
        private readonly object Result;

        public RequiredIfAttribute(string Properties, Object Result)
        {
            this.Properties = Properties;
            this.Result = Result;
            ErrorMessage = DefaultErrorMessageFormatString;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            object propertyValue = type.GetProperty(Properties).GetValue(instance, null);
            if (propertyValue == null || value != Result)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(context.DisplayName + " requerido. ");
        }
    }
}
