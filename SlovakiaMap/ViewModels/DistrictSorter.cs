using SlovakiaMap.Models;
using SlovakiaMap.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SlovakiaMap.ViewModels
{
    public class DistrictSorter
    {
        public void SortDistrictByProperty(string propertyName, List<District> districts)
        {
            dynamic? max = districts.Max(d => ReflectionTools.GetPropValue(d, propertyName));
            if (max is null)
                return;

            foreach (District dist in districts)
            {
                dynamic? prop = ReflectionTools.GetPropValue(dist, propertyName);
                if (prop is null) continue;

                var col = (byte)(prop * 255 / max);
                Color color = Color.FromArgb(255, 50, 50, 50);

                color = Color.FromArgb(color.A, col, (byte)(255 - col), color.B);
                Brush brush = new SolidColorBrush(color);
                dist.Fill = brush;
            }
        }

        public void SortByRegion(List<District> districts)
        {
            var color = Color.FromArgb(255, 50, 50, 255);

            List<District> sortedDistrictsList = districts.ToList();
            sortedDistrictsList = (from m in sortedDistrictsList
                             orderby m.Region ascending
                             select m)
                                .ToList();

            var prevRegion = "";
            foreach (District dist in sortedDistrictsList)
            {
                if (dist.Region != prevRegion)
                    color = Color.FromArgb(color.A, color.R, (byte)(color.G + 25), color.B);
                Brush brush = new SolidColorBrush(color);
                dist.Fill = brush;
                prevRegion = dist.Region;
            }
        }

    }
}