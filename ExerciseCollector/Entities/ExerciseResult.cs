namespace ExerciseCollector.Entities;

public class ExerciseResult
{
    required public Exercise Exercise { get; set; }
    
    public int Result { get; set; }
}
