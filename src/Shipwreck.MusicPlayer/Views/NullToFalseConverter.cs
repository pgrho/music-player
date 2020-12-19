using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shipwreck.MusicPlayer.Views
{
    internal sealed class NullToFalseConverter : BooleanConverterBase
    {
        protected override bool ConvertCore(object value, object parameter, CultureInfo culture)
            => value != null;
    }
}