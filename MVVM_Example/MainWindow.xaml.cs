using System.Windows;
using MVVM_Example.ViewModels;

namespace MVVM_Example
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TaskViewModel();
        }
    }
}
