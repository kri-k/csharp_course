using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Lab3
{
    public partial class MainWindow : Window
    {

        MainViewModel _mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
        }
    }
}
