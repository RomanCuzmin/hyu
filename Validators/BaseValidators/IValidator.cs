using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Validators.BaseValidators
{
    public interface IValidator <T> : INotifyDataErrorInfo
    {
        public void ValidateProperty(string propertyName, T instance);
        public void ValidateAll(T  instance);
        public void ClearErrors(string propertyName = null);
        public bool IsPropertyValid(string propertyName);
    }
}
