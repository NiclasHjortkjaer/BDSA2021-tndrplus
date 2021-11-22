namespace Core;

public record StudentDto (
    int Id,
    Degree CurrentDegree,
    Task<IEnumerable<ProjectDto>> SavedProjects,
    float AcquiredETCS,
    Degree StudyLevel
) : AccountDto (Id, CurrentDegree);

public record StudentCreateDto : AccountCreateDto {
    public IEnumerable<ProjectDto> SavedProjects { get; init; }

    public float AcquiredETCS { get; init; }

    public Degree StudyLevel { get; init; }
}

public record StudentUpdateDto : AccountCreateDto{
    public int Id { get; init; }
}