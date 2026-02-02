using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.ValidationRules.BaseValidationRules
{
    public interface IValidationRule <T>
    {
        string PropertyName {  get; }
        string ErrorMessage { get; }
        bool Validate(T obj);
    }
}
