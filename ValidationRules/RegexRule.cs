using BankKapital.ValidationRules.BaseValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankKapital.ValidationRules
{
    public  class RegexRule <T> : ValidationRule<T>
    {
        private readonly Func<T, string> _valueSelector;
        private readonly string _pattern;

        public RegexRule(string propertyName, Func <T, string> valueSelector, string pattern,
            string errorMessage = null)
        {
            PropertyName = propertyName;
            _valueSelector = valueSelector;
            _pattern = pattern;
            ErrorMessage = errorMessage ?? $"Неверный формат {propertyName}";
        }

        public override bool Validate(T obj)
        {
            var value = _valueSelector(obj);
            return Regex.IsMatch(value, _pattern);
        }
    }
    
    
}
