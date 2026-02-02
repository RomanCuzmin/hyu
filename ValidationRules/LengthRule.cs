using BankKapital.ValidationRules.BaseValidationRules;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules
{
    public class LengthRule<T> : ValidationRule<T>
    {
        private readonly Func<T, string> _valueSelector;
        private readonly int _minLength;
        private readonly int _maxLength;

        public LengthRule(string propertyName, Func<T, string> valueSelector,
                          int minLength, int maxLength, string errorMessage = null)
        {
            PropertyName = propertyName;
            _valueSelector = valueSelector;
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = errorMessage ?? $"{propertyName} должен содержать от {minLength} до {maxLength} символов";
        }

        public override bool Validate(T obj)
        {
            var value = _valueSelector(obj);
            if (string.IsNullOrEmpty(value)) return true;
            return value.Length >= _minLength && value.Length <= _maxLength;
        }
    }
}
