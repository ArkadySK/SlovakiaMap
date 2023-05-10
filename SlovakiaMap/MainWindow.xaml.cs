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
