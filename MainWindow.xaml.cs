using BankCapital.Services;
using BankKapital.Services;
using BankKapital.ViewModels;
using System.Windows;

namespace BankKapital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///    public partial class MainWindow : Window
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Создаем сервисы
            IUserService userService = new UserService();

            // Создаем главную ViewModel
            _mainViewModel = new MainViewModel(userService);

            // Устанавливаем DataContext
            DataContext = _mainViewModel;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void closeBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
    //public partial class MainWindow : Window
    //{
    //    private readonly User _user;
    //    private RegistrationViewModel _registrationViewModel;

    //    public MainWindow()
    //    {
    //        InitializeComponent();
    //        _registrationViewModel = new RegistrationViewModel();
    //        registrationTab.DataContext = _registrationViewModel;


    //    }

    //    public User GettingTheUserBeforeLoggingIn()
    //    {
    //        return UserService.UserSearchByLogin(loginToLogInTB.Text);
    //    }
    //    public void ShowTheRegistrationTab()
    //    {
    //        loginTab.IsEnabled = false;
    //        registrationTab.IsEnabled = true;
    //        registrationTab.IsSelected = true;
    //    }

    //    private void LogInDTB_Click(object sender, RoutedEventArgs e)
    //    {
         
    //    }

    //    private void RegistrationBTN_Click(object sender, RoutedEventArgs e)
    //    {
    //        ShowTheRegistrationTab();
    //    }


    //    private void RegistrarBTN_Click(object sender, RoutedEventArgs e)
    //    {
    //        // Проверяем все поля перед регистрацией
    //        _registrationViewModel.ValidateAll();

    //        if (_registrationViewModel.IsValid)
    //        {
    //            // Логика регистрации
    //            MessageBox.Show("Регистрация успешна!", "Успех",
    //                MessageBoxButton.OK, MessageBoxImage.Information);

    //            // Переходим на главную вкладку
    //            //MainTab.IsEnabled = true;
    //            //registrationTab.Width = 0;
    //            //registrationTab.Height = 0;
    //        }
    //        else
    //        {
    //            MessageBox.Show("Пожалуйста, исправьте ошибки в форме.", "Ошибка",
    //                MessageBoxButton.OK, MessageBoxImage.Warning);
    //        }
    //    }

    //}
    
