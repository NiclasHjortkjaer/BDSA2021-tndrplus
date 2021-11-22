namespace Core;

public record SupervisorDto (
    int Id,
    Degree CurrentDegree,
    IEnumerable<ProjectDto> SavedProjects
) : AccountDto (Id, CurrentDegree);

public record SupervisorCreateDto : AccountCreateDto {
    public IEnumerable<ProjectDto> Projects { get; init; }
}

public record SupervisorUpdateDto : AccountCreateDto{
    public int Id {get; init;}
}