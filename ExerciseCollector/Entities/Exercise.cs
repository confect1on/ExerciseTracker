using ExerciseCollector.Entities.Enums;

namespace ExerciseCollector.Entities;

public class Exercise
{
    required public ExerciseType ExerciseType { get; set; }

    public Gender Gender { get; set; }
    
    required public string Name { get; set; }
    
    required public string UnitOfMeasurementName { get; set; }
    
    required public SortedList<decimal, int> Results { get; set; }
}
