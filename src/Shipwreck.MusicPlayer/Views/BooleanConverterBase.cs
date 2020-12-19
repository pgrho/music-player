using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shipwreck.MusicPlayer.Views
{
    internal abstract class BooleanConverterBase : IValueConverter
    {
        public object TruePart { get; set; } = DependencyProperty.UnsetValue;
        public object FalsePart { get; set; } = DependencyProperty.UnsetValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = ConvertCore(value, parameter, culture);
            var v = b ? TruePart : FalsePart;
            if (v == DependencyProperty.UnsetValue)
            {
                if (targetType == typeof(Visibility))
                {
                    return b ? Visibility.Visible : Visibility.Collapsed;
                }
                return b;
            }
            return v;
        }

        protected abstract bool ConvertCore(object value, object parameter, CultureInfo culture);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new System.NotSupportedException();
    }
}