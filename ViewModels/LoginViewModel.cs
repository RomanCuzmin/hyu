using BankKapital.Commands;
using BankKapital.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankKapital.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _errorMessage;

        private readonly IUserService _userService;
        private readonly Action<string> _onLoginSucces;

        public LoginViewModel(IUserService userService, Action<string> onLoginSucces)
        {
            _userService = userService;
            _onLoginSucces = onLoginSucces;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }


        public void ExecuteLogin(object parameter)
        {
            var user = _userService.Authenticate(Login, Password);

            if (user != null)
            {
                _onLoginSucces?.Invoke($"{user.Name} {user.SerName}");

            }

            else
            {
                ErrorMessage = "Неверный логин или пароль";
            }
        }

        # region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
