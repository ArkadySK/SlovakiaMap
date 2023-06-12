using SlovakiaMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SlovakiaMap.ViewModels
{
    public class SlovakiaViewModel : INotifyPropertyChanged
    {
        public Dictionary<string, District> Districts
        {
            get => districts;
        }
        public District? SelectedDistrict
        {
            get => selectedDistrict;
            set
            {
                selectedDistrict = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private District? selectedDistrict;

        private readonly Dictionary<string, District> districts = new();

        public async Task LoadDistricts(string filePath)
        {
            Districts.Clear();
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
            var color = Color.FromArgb(255, 50, 50, 50);

            List<District> districtsList = districts.Values.ToList();
            districtsList = (from m in districtsList
                             orderby m.ResidentsCount ascending
                             select m)
                                .ToList();

            foreach (District muni in districtsList)
            {
                color = Color.FromArgb(color.A, (byte)(color.R + 2), color.G, color.B);
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
            Districts.Add(spz, new District(name, kraj, spz, obyv));
        }
    }
}
