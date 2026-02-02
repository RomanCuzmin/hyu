using BankKapital.ValidationRules.BaseValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules
{
    public class RangeRule<T> : ValidationRule<T>
    {
        private readonly Func<T, int> _valueSelector;
        private readonly int _min;
        private readonly int _max;

        public RangeRule(string properttyName, Func<T,int> valueSelector, int min, int max,
            string errorMessage = null)
        {
            PropertyName = properttyName;
            _valueSelector = valueSelector;
            _min = min;
            _max = max;
            ErrorMessage = errorMessage ?? $"{properttyName} должен быть от {min} до {max}";
        }

        public override bool Validate(T obj)
        {
            var value = _valueSelector(obj);
            return value >= _min && value <= _max;
        }
    }
}
