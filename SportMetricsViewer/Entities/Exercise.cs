using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.Entities;

public class Exercise
{
    public int Id { get; set; }

    public required ExerciseType ExerciseType { get; set; }

    public Gender Gender { get; set; }
    
    public ExerciseEntrantType ExerciseEntrantType { get; set; }
    
    public required string Name { get; set; }
    
    public required string UnitOfMeasurementName { get; set; }
    
    public required SortedList<decimal, int> Results { get; set; }
}
