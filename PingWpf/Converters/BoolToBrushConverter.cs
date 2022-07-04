using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PingWpf.Converters
{
    [ValueConversion(typeof(bool?), typeof(Brush))]
    class BoolToBrushConverter : IValueConverter
    {
        static Brush falseBrush;
        static Brush trueBrush;
        static Brush nullBrush;

        public Brush FalseBrush { get { return falseBrush ?? (falseBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xe4, 0xe4))); } }
        public Brush TrueBrush { get { return trueBrush ?? (trueBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xf5, 0xff, 0xfa))); } }
        public Brush NullBrush { get { return nullBrush ?? (nullBrush = new SolidColorBrush(Color.FromArgb(0x00, 0xff, 0xff, 0xff))); } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? b = value as bool?;
            if (b == null) return null;
            if (b.HasValue)
                return b.Value ? TrueBrush : FalseBrush;
            return NullBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
