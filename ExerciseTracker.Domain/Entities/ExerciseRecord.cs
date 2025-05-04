namespace ExerciseTracker.Domain.Entities;

public class ExerciseRecord
{
    public required int ExerciseId { get; set; }
    
    public decimal ResultInMeasurableUnit { get; set; }
}
