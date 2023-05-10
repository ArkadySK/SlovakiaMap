using SlovakiaMap.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SlovakiaMap
{
    public class SlovakiaViewModel : INotifyPropertyChanged
    {
        private Municipality selectedMunicipality;

        private Dictionary<string, Municipality> municipatilies = new Dictionary<string, Municipality>();

        public Dictionary<string, Municipality> Municipatilies
        {
            get => municipatilies;
            set
            {
                municipatilies = value;
            }
        }
        public Municipality SelectedMunicipality
        {
            get => selectedMunicipality;
            set
            {
                selectedMunicipality = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadDistricts(string filePath)
        {
            Municipatilies.Clear();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var file = new StreamReader(filePath, Encoding.GetEncoding("windows-1250"), true);

            //var color = Colors.Gray;
            string data = await file.ReadToEndAsync();
            foreach (string line in data.Split("\n"))
            {
                try
                {
                    CreateDistrict(line);
                }
                catch { }
            }
            file.Close();
            await Task.CompletedTask;

            SortMunicipalitiesByResidents();
        }

        private void SortMunicipalitiesByResidents()
        {
            var color = System.Windows.Media.Color.FromArgb(255, 50, 50, 50);

            List<Municipality> municipilityList = municipatilies.Values.ToList();
            municipilityList = (from m in municipilityList
                                orderby m.ResidentsCount ascending
                                select m)
                                .ToList();

            foreach (Municipality muni in municipilityList)
            {
                color = System.Windows.Media.Color.FromArgb(color.A, (byte)(color.R + 2), color.G, color.B);
                Brush brush = new SolidColorBrush(color);
                muni.Fill = brush;
            }
        }

        private void CreateDistrict(string lineInfo)
        {
            var infos = lineInfo.Split(";");
            var name = infos[0];
            var spz = infos[1];
            var kraj = infos[3];
            int obyv = int.Parse(infos[4].Replace(" ", ""));
            Municipatilies.Add(spz, new Municipality(name, kraj, spz, obyv));
        }
    }
}
