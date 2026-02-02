using BankCapital.Services;
using BankKapital.Models;
using BankKapital.Services;
using BankKapital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankKapital
{
    /// <summary>
    /// Логика взаимодействия для RegistrationOrLoginWindow.xaml
    /// </summary>
    public partial class RegistrationOrLoginWindow : Window
    {
        private readonly User _user;
        private RegistrationViewModel _registrationViewModel;
        private IUserService userService;
        private Action onRegistrationSuccess;
        public RegistrationOrLoginWindow()
        {
            InitializeComponent();
            _registrationViewModel = new RegistrationViewModel(userService, onRegistrationSuccess);
            registrationTab.DataContext = _registrationViewModel;
            userService = new UserService();
        }

        private void RegistrationBTN_Click(object sender, RoutedEventArgs e)
        {
            loginTab.IsEnabled = false;
            registrationTab.IsEnabled = true;
            registrationTab.IsSelected = true;
        }

        private void RegistrarBTN_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем все поля перед регистрацией
            _registrationViewModel.ValidateAll();

            

            if (_registrationViewModel.IsValid)
            {
                // Логика регистрации
                MessageBox.Show("Регистрация успешна!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                userService.RegisterUser(_registrationViewModel);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Переходим на главную вкладку
                //MainTab.IsEnabled = true;
                //registrationTab.Width = 0;
                //registrationTab.Height = 0;
            }
            else
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в форме.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

 
        }
    }
}
