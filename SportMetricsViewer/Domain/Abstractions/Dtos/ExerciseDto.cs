using SportMetricsViewer.Entities.Enums;

namespace SportMetricsViewer.Domain.Abstractions.Dtos;

public record ExerciseDto(int Id, string Name, string UnitOfMeasurementName, ExerciseType ExerciseType);
