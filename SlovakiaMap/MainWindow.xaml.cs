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
        public SlovakiaViewModel Slovakia { get; set; } = new SlovakiaViewModel();

        public Window1()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Slovakia.LoadDistricts("SK_Okresy.txt");
            this.DataContext = Slovakia;
        }

        private void Municipality_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is not Path)
                return;
            var senderPath = (Path)sender;
            Slovakia.SelectedMunicipality = Slovakia.Municipatilies[senderPath.Name];
        }
    }
}
