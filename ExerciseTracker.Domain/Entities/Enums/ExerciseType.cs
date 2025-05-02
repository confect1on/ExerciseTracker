using System.ComponentModel.DataAnnotations;

namespace ExerciseTracker.Domain.Entities.Enums;

public enum ExerciseType
{
    [Display(Name = "Сила")]
    Strength,
    [Display(Name = "Быстрота и ловкость")]
    Agility,
    [Display(Name = "Выносливость")]
    Endurance
}
