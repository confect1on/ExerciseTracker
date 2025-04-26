using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportMetricsViewer.Entities.Enums;

public enum ExerciseType
{
    [Display(Name = "Сила")]
    Strength,
    [Display(Name = "Быстрота и ловкость")]
    Agility,
    [Display(Name = "Выносливость")]
    Endurance
}
