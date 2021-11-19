namespace Core;

public record StudentDto (
    Task<IEnumerable<ProjectDto>> SavedProjects,
    float AcquiredETCS,
    Level StudyLevel
) : AccountDto;

public record StudentCreateDto : AccountCreateDto {
    public IEnumerable<ProjectDto> SavedProjects { get; init; }

    public float AcquiredETCS{get; init;}

    public Level StudyLevel{get; init;}
}

public record StudentUpdateDto : AccountCreateDto{
    public int Id {get; init;}
}