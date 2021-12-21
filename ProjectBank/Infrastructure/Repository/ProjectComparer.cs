namespace ProjectBank.Infrastructure.Repository;

public class ProjectComparer : IEqualityComparer<ProjectDetailsDto>
{
    public bool Equals(ProjectDetailsDto? x, ProjectDetailsDto? y)
        => y != null && x != null && x.Id == y.Id;
    
    public int GetHashCode(ProjectDetailsDto obj)
        => obj.Id.GetHashCode();
}