using System.ComponentModel.DataAnnotations;

namespace ExerciseCollector.Entities.Enums;

public enum ExerciseType
{
    [Display(Name = "Сила")]
    Strength,
    [Display(Name = "Быстрота и ловкость")]
    Agility,
    [Display(Name = "Выносливость")]
    Endurance
}
