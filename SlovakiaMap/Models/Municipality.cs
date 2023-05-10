using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace SlovakiaMap.Models
{
    public class Municipality : INotifyPropertyChanged
    {
        private Brush mouseOverFill;
        private Brush fill;

        public string Name { get; }
        public string Region { get; }
        public string SPZ { get; }
        public int ResidentsCount { get; }
        public Brush Fill
        {
            get => fill;
            set
            {
                fill = value;
                NotifyPropertyChanged();
            }
        }

        public Municipality(string name, string region, string spz, int residentsCount)
        {
            Name = name;
            Region = region;
            SPZ = spz;
            ResidentsCount = residentsCount;
            Fill = Brushes.Gray;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string callerName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
        }
    }
}
