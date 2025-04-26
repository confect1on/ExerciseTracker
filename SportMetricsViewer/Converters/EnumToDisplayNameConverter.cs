using System.Globalization;
using SportMetricsViewer.Extensions;

namespace SportMetricsViewer.Converters;

public class EnumToDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            return enumValue.GetDisplayName();
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string displayedName)
        {
            return Enum
                .GetValues(targetType)
                .Cast<Enum>()
                .FirstOrDefault(enumValue => enumValue.GetDisplayName() == displayedName);
        }
        return null;
    }
}
