using SlovakiaMap.Models;
using SlovakiaMap.Tools;
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
        }

        public void SortDistrictByProperty(string propertyName)
        {
            List<District> districtsList = districts.Values.ToList();
            dynamic? max = districtsList.Max(d => ReflectionTools.GetPropValue(d, propertyName));
            if (max is null)
                return;

            foreach (District dist in districtsList)
            {
                dynamic? prop = ReflectionTools.GetPropValue(dist, propertyName);
                if (prop is null) continue;

                var col = (byte)(prop * 255 / max);
                Color color = Color.FromArgb(255, 50, 50, 50);

                color = Color.FromArgb(color.A, col, color.G, color.B);
                Brush brush = new SolidColorBrush(color);
                dist.Fill = brush;
            }
        }

        public void SortByRegion()
        {
            var color = Color.FromArgb(255, 50, 50, 255);

            List<District> districtsList = districts.Values.ToList();
            districtsList = (from m in districtsList
                             orderby m.Region ascending
                             select m)
                                .ToList();

            var prevRegion = "";
            foreach (District dist in districtsList)
            {
                if (dist.Region != prevRegion)
                    color = Color.FromArgb(color.A, color.R, (byte)(color.G + 25), color.B);
                Brush brush = new SolidColorBrush(color);
                dist.Fill = brush;
                prevRegion = dist.Region;
            }
        }

        private void CreateDistrict(string lineInfo)
        {
            var infos = lineInfo.Split(";");
            var name = infos[0];
            var spz = infos[1];
            var kraj = infos[3];
            int obyv = int.Parse(infos[4].Replace(" ", ""));
            double area = double.Parse(infos[6].Replace(" ", ""));
            Districts.Add(spz, new District(name, kraj, spz, area, obyv));
        }
    }
}
