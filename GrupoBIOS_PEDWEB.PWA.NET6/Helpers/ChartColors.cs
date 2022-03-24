using System;
namespace GrupoBIOS_PEDWEB.PWA.NET6.Helpers
{
    public static class ChartColors
    {
        private static readonly Lazy<IReadOnlyList<System.Drawing.Color>> _all = new(() => new System.Drawing.Color[7] { Red, Blue, Yellow, Orange, Green, Purple, Grey });

        public static IReadOnlyList<System.Drawing.Color> All => _all.Value;

        public static readonly System.Drawing.Color Red = System.Drawing.Color.FromArgb(248, 203, 173);
        public static readonly System.Drawing.Color Orange = System.Drawing.Color.FromArgb(255, 223, 159);
        public static readonly System.Drawing.Color Yellow = System.Drawing.Color.FromArgb(255, 255, 153);
        public static readonly System.Drawing.Color Green = System.Drawing.Color.FromArgb(204, 255, 153);
        public static readonly System.Drawing.Color Blue = System.Drawing.Color.FromArgb(180, 199, 231);
        public static readonly System.Drawing.Color Purple = System.Drawing.Color.FromArgb(228, 177, 233);
        public static readonly System.Drawing.Color Grey = System.Drawing.Color.FromArgb(203, 205, 207);
        public static readonly System.Drawing.Color Cyan = System.Drawing.Color.FromArgb(179, 232, 232);
    }
    public class PieData
    {
        public List<string> Labels { get; set; }
        public List<double> DatasetReal { get; set; }
        public List<double> DatasetEsperado { get; set; }
    }
}
