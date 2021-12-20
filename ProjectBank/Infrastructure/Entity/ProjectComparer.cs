using System.Diagnostics.CodeAnalysis;

namespace ProjectBank.Infrastructure.Entity;

public class ProjectComparer : IEqualityComparer<ProjectDetailsDto>
{
    public bool Equals(ProjectDetailsDto? x, ProjectDetailsDto? y)
        => x.Id == y.Id;
    
    public int GetHashCode([DisallowNull] ProjectDetailsDto obj)
        => obj.Id.GetHashCode();
}