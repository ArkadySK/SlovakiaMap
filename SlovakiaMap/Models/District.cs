using System;
using System.ComponentModel;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace SlovakiaMap.Models
{
    public class District : INotifyPropertyChanged
    {
        private Brush fill = Brushes.Gray;
        public string Name { get; }
        public string Region { get; }
        public string SPZ { get; }
        public double Area { get; }
        public int ResidentsCount { get; }
        public double ResidentsDensity
        {
            get => ((double)ResidentsCount / Area);
        }
        public double ReferentialDensity
        {
            get => Math.Log10((double)ResidentsCount / Area);
        }

        public Brush Fill
        {
            get => fill;
            set
            {
                fill = value;
                NotifyPropertyChanged();
            }
        }

        public District(string name, string region, string spz, double area, int residentsCount)
        {
            Name = name;
            Region = region;
            SPZ = spz;
            Area = area;
            ResidentsCount = residentsCount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string callerName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
        }
    }
}
