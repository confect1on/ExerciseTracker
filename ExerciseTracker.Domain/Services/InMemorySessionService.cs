using System.Collections.Concurrent;
using ExerciseTracker.Domain.Abstractions;
using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Services;

internal sealed class InMemorySessionService : ISessionService
{
    private readonly ConcurrentDictionary<Guid, IReadOnlyCollection<ExerciseRecord>> _sessionIdToExerciseRecords = new();

    public Guid SaveSession(IReadOnlyCollection<ExerciseRecord> exerciseRecords)
    {
        var sessionId = Guid.NewGuid();
        if (!_sessionIdToExerciseRecords.TryAdd(sessionId, exerciseRecords))
        {
            throw new InvalidOperationException($"Session {sessionId} has already been saved.");
        }
        return sessionId;
    }

    public IReadOnlyCollection<ExerciseRecord> LoadSession(Guid sessionId)
    {
        if (!_sessionIdToExerciseRecords.TryGetValue(sessionId, out var exerciseRecords))
        {
            throw new InvalidOperationException($"Session {sessionId} does not exist.");
        }
        return exerciseRecords;
    }
}
