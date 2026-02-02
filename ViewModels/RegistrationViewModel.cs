using BankKapital.Commands;
using BankKapital.Services;
using BankKapital.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace BankKapital.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanging, INotifyDataErrorInfo
    {
        private string _name;
        private string _surName;
        private string _loginName;
        private string _password;
        private string _confirmPassword;
        private string _phoneNumber;
        private string _passportSeries;
        private string _passportNumber;

        private readonly ValidationBeforeRegistration _validator;
        private ObservableCollection<string> _errorMessages = new ObservableCollection<string>();
        private readonly IUserService _userService;
        private readonly Action _onRegistrationSuccess;


        public ICommand RegisterCommand { get; }
        public ICommand ClearCommand { get; }

        public event EventHandler<string> RegistrationCompleted;
        public event EventHandler<string> RegistrationFailed;

        public RegistrationViewModel(IUserService userService, Action onRegistrationSuccess)
        {
            _userService = userService;
            _onRegistrationSuccess = onRegistrationSuccess;
            _validator = new ValidationBeforeRegistration();
            _validator.ErrorsChanged += (s, e) =>
            {
                ErrorsChanged?.Invoke(this, e);
                OnPropertyChanged(nameof(IsValid));
                UpdateErrorMessages();
            };

            RegisterCommand = new RelayCommand(
                execute: ExecuteRegister,
                canExecute: CanExecuteRegister);

            //ClearCommand = new RelayCommand(
            //    execute: ExecuteClear);
        }

        private bool CanExecuteRegister(object parametr)
        {
            return IsValid;
        }

        private void ExecuteRegister(object parametr)
        {
            ValidateAll();

            if (!IsValid)
            {
                RegistrationFailed?.Invoke(this, "Пожалуйста, исправьте ошибки в  форме");
                return;
            }

            if (!_userService.CheckingTheUserForNewData(this))
            {
                RegistrationFailed?.Invoke(this, "Пользователь с такими данными уже существует");
                return;
            }

            var success = _userService.RegisterUser(this);

            if (success)
            {
                ExecuteClear(null);

                RegistrationCompleted?.Invoke(this, "Регистрация успешно завершена!");

                _onRegistrationSuccess?.Invoke();
            }

            else
            {
                RegistrationFailed?.Invoke(this, "Ошибка при регистрации пользователя");
            }
        }

        private void ExecuteClear(object parametr)
        {
            Name = string.Empty;
            SurName = string.Empty;
            LoginName = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            PhoneNumber = string.Empty;
            PassportSeries = string.Empty;
            PassportNumber = string.Empty;
            ClearAllErrors();
        }


        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(Name), this);
                }
            }
        }

        public string SurName
        {
            get => _surName;
            set
            {
                if (_surName != value)
                {
                    _surName = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(SurName), this);
                }
            }
        }

        public string LoginName
        {
            get => _loginName;
            set
            {
                if (_loginName != value)
                {
                    _loginName = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(LoginName), this);
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(Password), this);
                    // Также проверяем подтверждение пароля при изменении пароля
                    _validator.ValidateProperty(nameof(ConfirmPassword), this);
                }
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(ConfirmPassword), this);
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(PhoneNumber), this);
                }
            }
        }

        public string PassportSeries
        {
            get => _passportSeries;
            set
            {
                if (_passportSeries != value)
                {
                    _passportSeries = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(PassportSeries), this);
                }
            }
        }

        public string PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (_passportNumber != value)
                {
                    _passportNumber = value;
                    OnPropertyChanged();
                    _validator.ValidateProperty(nameof(PassportNumber), this);
                }
            }
        }

        public bool IsValid => !_validator.HasErrors;

        public FlowDocument FormattedErrors
        {
            get
            {
                var doc = new FlowDocument();
                var paragraph = new Paragraph();

                if (!HasErrors)
                {
                    paragraph.Inlines.Add(new Run("Нет ошибок")
                    {
                        Foreground = System.Windows.Media.Brushes.Gray
                    });
                }

                else
                {
                    var allErrors = _validator.GetErrors(null) as IEnumerable<string>;
                    if (allErrors != null)
                    {
                        foreach (var error in allErrors)
                        {
                            var bulet = new Run(".")
                            {
                                Foreground = System.Windows.Media.Brushes.Red,
                                FontWeight = FontWeights.Bold
                            };


                            var errorText = new Run(error)
                            {
                                Foreground = System.Windows.Media.Brushes.DarkRed
                            };

                            paragraph.Inlines.Add(bulet);
                            paragraph.Inlines.Add(errorText);
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                }
                doc.Blocks.Add(paragraph);
                return doc;
            }
        }

        private void UpdateErrorMessages()
        {
            var allErrors = _validator.GetErrors(null) as System.Collections.IEnumerable;
            if (allErrors != null)
            {
                _errorMessages.Clear();
                foreach (string error in allErrors)
                {
                    _errorMessages.Add(error);
                }
            }
            OnPropertyChanged(nameof(ErrorMessages));
            OnPropertyChanged(nameof(HasErrors));
        }

        public ObservableCollection<string> ErrorMessages => _errorMessages;

        //public string AllErrorsText
        //{
        //    get
        //    {
        //        if (!HasErrors)
        //            return string.Empty;

        //        var allErrors = _validator.GetErrors(null) as IEnumerable<string>;

        //        if (allErrors == null)
        //            return string.Empty;

        //        var sb = new StringBuilder();

        //        foreach (var error in allErrors)
        //        {
        //            sb.AppendLine($". {error}");
        //        }

        //        return sb.ToString();
        //    }
        //}
        public string AllErrorsText
        {
            get
            {
                if (_errorMessages.Count == 0)
                    return "Нет ошибок";

                return string.Join("\n", _errorMessages.Select(e => $"• {e}"));
            }
        }
        #region INotifyDataErrorInfo Implementation
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return _validator.GetErrors(propertyName);
        }

        public bool HasErrors => _validator.HasErrors;
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        // Метод для принудительной валидации всех полей
        public void ValidateAll()
        {
            _validator.ValidateAll(this);
        }

        // Метод для очистки всех ошибок
        public void ClearAllErrors()
        {
            _validator.ClearErrors();
        }
    }
}