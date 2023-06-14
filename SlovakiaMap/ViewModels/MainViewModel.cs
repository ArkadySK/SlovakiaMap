using CommunityToolkit.Mvvm.Input;
using SlovakiaMap.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SlovakiaMap.ViewModels
{
    public class MainViewModel
    {
        public ICommand AreaCommand { get; }
        public ICommand ResidentsCountCommand { get; }
        public ICommand ResidentsDensityCommand { get; }

        public SlovakiaViewModel Slovakia { get; } //TODO: get only?

        public MainViewModel()
        {
            AreaCommand = new RelayCommand(Area);
            ResidentsCountCommand = new RelayCommand(ResidentsCount);
            ResidentsDensityCommand = new RelayCommand(ResidentsDensity);
            Slovakia = new SlovakiaViewModel();
        }

        public async Task Load(string filePath)
        {
            await Slovakia.LoadDistricts(filePath);
        }

        private void Area()
        {

        }

        private void ResidentsCount()
        {

        }

        private void ResidentsDensity()
        {

        }
    }
}
