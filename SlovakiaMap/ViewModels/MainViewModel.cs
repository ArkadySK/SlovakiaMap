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
        // TODO: implement ICommand to a class
        public ICommand AreaCommand { get; set; }
        public ICommand ResidentsCountCommand { get; set; }
        public ICommand ResidentsDensityCommand { get; set; }

        public SlovakiaViewModel Slovakia { get; set; } //TODO: get only?

        public async Task Load(string filePath)
        {
            Slovakia = new SlovakiaViewModel();
            await Slovakia.LoadDistricts(filePath);
        }
    }
}
