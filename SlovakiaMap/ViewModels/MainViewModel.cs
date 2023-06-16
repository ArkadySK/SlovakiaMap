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
        public ICommand RegionCommand { get; }
        public ICommand ResidentsCountCommand { get; }
        public ICommand ResidentsDensityCommand { get; }

        public SlovakiaViewModel Slovakia { get; }

        public MainViewModel()
        {
            AreaCommand = new RelayCommand(Area);
            RegionCommand = new RelayCommand(Region);
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
            Slovakia.SortDistrictByProperty("Area");
        }

        private void Region()
        {
            Slovakia.SortByRegion();
        }

        private void ResidentsCount()
        {
            Slovakia.SortDistrictByProperty("ResidentsCount");
        }

        private void ResidentsDensity()
        {
            Slovakia.SortDistrictByProperty("ResidentsDensity");
        }
    }
}
