using BankKapital.ValidationRules.BaseValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules
{
    public class RequiredRule<T> : ValidationRule<T>
    {
        private readonly Func<T, string> _valueSelector;

        public RequiredRule(string propertyName, Func<T, string> valueSelector,
            string errorMessage = null)
        {
            PropertyName = propertyName;
            _valueSelector = valueSelector;
            ErrorMessage = errorMessage ?? $"{propertyName} обязательно для заполнения";
        }

        public override bool Validate(T obj)
        {
            var value = _valueSelector(obj);
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
