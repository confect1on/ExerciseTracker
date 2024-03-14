using ExerciseCollector.Entities.Enums;

namespace ExerciseCollector.Entities;

public class Exercise
{
    public int Id { get; set; }

    required public ExerciseType ExerciseType { get; set; }

    public Gender Gender { get; set; }
    
    public ExerciseEntrantType ExerciseEntrantType { get; set; }
    
    required public string Name { get; set; }
    
    required public string UnitOfMeasurementName { get; set; }
    
    required public SortedList<decimal, int> Results { get; set; }
}
