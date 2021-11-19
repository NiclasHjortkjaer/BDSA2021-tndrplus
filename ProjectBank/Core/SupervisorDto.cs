namespace Core;

public record SupervisorDto (
    IEnumerable<ProjectDto> SavedProjects
) : AccountDto;

public record SupervisorCreateDto : AccountCreateDto {
    public IEnumerable<ProjectDto> Projects { get; init; }
}

public record SupervisorUpdateDto : AccountCreateDto{
    public int Id {get; init;}
}