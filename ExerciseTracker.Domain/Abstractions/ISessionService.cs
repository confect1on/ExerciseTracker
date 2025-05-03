using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Abstractions;

public interface ISessionService
{
    Guid SaveSession(IReadOnlyCollection<ExerciseRecord> exerciseRecords);
    
    IReadOnlyCollection<ExerciseRecord> LoadSession(Guid sessionId);
}
