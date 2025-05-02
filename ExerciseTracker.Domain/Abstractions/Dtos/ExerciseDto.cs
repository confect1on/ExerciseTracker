using ExerciseTracker.Domain.Entities.Enums;

namespace ExerciseTracker.Domain.Abstractions.Dtos;

public record ExerciseDto(int Id, string Name, string UnitOfMeasurementName, ExerciseType ExerciseType)
{
    public virtual bool Equals(ExerciseDto? other)
    {
        return other is not null && Id == other.Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
};
