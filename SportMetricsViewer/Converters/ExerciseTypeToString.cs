using System.Globalization;
using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.Converters;

public class ExerciseTypeToString : IValueConverter
{
    private readonly static IDictionary<ExerciseType, string> exerciseTypeToString = new Dictionary<ExerciseType, string>
    {
        { ExerciseType.Agility,  "Быстрота и ловкость"},
        { ExerciseType.Endurance, "Выносливость"},
        { ExerciseType.Strength, "Сила"}
    };

    private readonly static IDictionary<string, ExerciseType> stringToExerciseType =
        exerciseTypeToString
            .ToDictionary(x => x.Value, x => x.Key);

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);
        return exerciseTypeToString[(ExerciseType)value];
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);
        return stringToExerciseType[(string)value];
    }
}
