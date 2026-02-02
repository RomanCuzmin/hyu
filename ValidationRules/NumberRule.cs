using BankKapital.ValidationRules.BaseValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules
{
    public  class NumberRule<T> : ValidationRule<T>
    {
        private readonly Func<T,string> _valueSelector;

        public NumberRule (string propertyName, Func<T, string> valueSelector, string errorMessage)
        {
            PropertyName = propertyName;
            _valueSelector = valueSelector;
            ErrorMessage = errorMessage ?? $"{propertyName} должен быть числом";
        }

        public override bool Validate(T obj)
        {
            var value = _valueSelector(obj);
            return int.TryParse(value, out var result);
        }
    }
}
