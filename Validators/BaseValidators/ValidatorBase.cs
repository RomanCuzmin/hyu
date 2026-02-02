using BankKapital.ValidationRules.BaseValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Validators.BaseValidators
{
    public abstract class ValidatorBase<T> : IValidator<T> where T : class
    {
        protected readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        protected readonly List<IValidationRule<T>> _validationRules = new List<IValidationRule<T>>();

        protected ValidatorBase()
        {
            InitializeRules();
        }

        protected abstract void InitializeRules();

        protected void AddRule(IValidationRule<T> rule)
        {
            _validationRules.Add(rule);
        }

        public void ValidateProperty(string propertyName, T instance)
        {
            ClearErrors(propertyName);

            var propertyRules = _validationRules
                .Where(r => r.PropertyName == propertyName)
                .ToList();

            foreach (var rule in propertyRules)
            {
                if (!rule.Validate(instance))
                {
                    AddError(propertyName, rule.ErrorMessage);
                }
            }
        }

        public void ValidateAll(T instance)
        {
            var groupedRules = _validationRules
                .GroupBy(r => r.PropertyName);

            foreach (var group in groupedRules)
            {
                ValidateProperty(group.Key, instance);
            }
        }

        public void ClearErrors(string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                var keys = _errors.Keys.ToList();
                _errors.Clear();
                foreach (var key in keys)
                {
                    OnErrorsChanged(key);
                }
            }
            else if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        public bool IsPropertyValid(string propertyName)
        {
            return !_errors.ContainsKey(propertyName) || !_errors[propertyName].Any();
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        #region INotifyDataErrorInfo Implementation
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _errors.SelectMany(e => e.Value).ToList();

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public bool HasErrors => _errors.Any();

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion
    }
}
