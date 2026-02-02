using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules.BaseValidationRules
{
    public abstract class ValidationRule<T> : IValidationRule<T>
    {
        public string PropertyName { get;protected set; }
        public string ErrorMessage { get; protected set; }

        public abstract bool Validate(T obj);
    }
    
    
}
