using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using SlovakiaMap.ViewModels;

namespace SlovakiaMap
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public MainViewModel ViewModel { get; set; } = new MainViewModel();

        public Window1()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Load("SK_Okresy.txt");
            this.DataContext = ViewModel;
        }

        private void Municipality_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is not Path)
                return;
            var senderPath = (Path)sender;
            ViewModel.Slovakia.SelectedDistrict = ViewModel.Slovakia.Districts[senderPath.Name];
        }
    }
}
