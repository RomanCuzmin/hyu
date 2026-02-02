using BankKapital.ValidationRules.BaseValidationRules;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules
{
    public class CustomRule<T> : ValidationRule<T>
    {
        private readonly Func<T, bool> _validationFunc;

        public CustomRule(string propertyName, Func<T, bool> validationFunc, string errorMessage)
        {
            PropertyName = propertyName;
            _validationFunc = validationFunc;
            ErrorMessage = errorMessage;
        }

        public override bool Validate(T obj)
        {
            return _validationFunc(obj);
        }
    }
}
