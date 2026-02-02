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
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private object _currentContent;
        private string _userName;
        private bool _isLoggedIn;

        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            LoginViewModel = new LoginViewModel(userService, OnLoginSuccess);
            RegistrationViewModel = new RegistrationViewModel(userService, OnRegistrationSuccess);
            CurrentContent = LoginViewModel;
        }

        public LoginViewModel LoginViewModel { get; }
        public RegistrationViewModel RegistrationViewModel { get; }
        public MainAppViewModel MainAppViewModel { get; private set; }

        public object CurrentContent
        {
            get => _currentContent;

            set
            {
                _currentContent = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;

            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogoutCommand => new RelayCommand(ExecuteLogout);
        public ICommand ShowRegistrationCommand => new RelayCommand(ExecuteShowRegistration);
        public ICommand ShowLoginCommand => new RelayCommand(ExecuteShowLogin);
        private void ExecuteShowRegistration(object parameter)
        {
            CurrentContent = RegistrationViewModel;
        }

        private void ExecuteShowLogin(object parameter)
        {
            CurrentContent = LoginViewModel;
        }

        private void ExecuteLogout(object parameter)
        {
            IsLoggedIn = false;
            UserName = null;
            CurrentContent = LoginViewModel;
        }

        private void OnLoginSuccess(string userName)
        {
            UserName = userName;
            IsLoggedIn = true;
            MainAppViewModel = new MainAppViewModel(userName);
            CurrentContent = MainAppViewModel;
        }

        private void OnRegistrationSuccess()
        {
            // После регистрации переходим на главную вкладку
            UserName = RegistrationViewModel.Name;
            IsLoggedIn = true;
            MainAppViewModel = new MainAppViewModel(UserName);
            CurrentContent = MainAppViewModel;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}