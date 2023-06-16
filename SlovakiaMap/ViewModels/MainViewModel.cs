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

        public DistrictSorter Sorter { get; }

        public MainViewModel()
        {
            AreaCommand = new RelayCommand(Area);
            RegionCommand = new RelayCommand(Region);
            ResidentsCountCommand = new RelayCommand(ResidentsCount);
            ResidentsDensityCommand = new RelayCommand(ResidentsDensity);
            Slovakia = new SlovakiaViewModel();
            Sorter = new DistrictSorter();
        }

        public async Task Load(string filePath)
        {
            await Slovakia.LoadDistricts(filePath);
        }

        private void Area()
        {
            Sorter.SortDistrictByProperty("Area", Slovakia.DistrictsList);
        }

        private void Region()
        {
            Sorter.SortByRegion(Slovakia.DistrictsList);
        }

        private void ResidentsCount()
        {
            Sorter.SortDistrictByProperty("ResidentsCount", Slovakia.DistrictsList);
        }

        private void ResidentsDensity()
        {
            Sorter.SortDistrictByProperty("ResidentsDensity", Slovakia.DistrictsList);
        }
    }
}
