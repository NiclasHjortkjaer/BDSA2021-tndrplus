namespace ProjectBank.Infrastructure.test;

public class ProjectComparerTests
{

    [Fact]
    public async Task Equals_return_true_given_similar_objects()
    {
        var ai1 = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"});
        var ai2 = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"});

        var ProjectComparer = new ProjectComparer();

        Assert.NotEqual(ai1, ai2);
        Assert.True(ProjectComparer.Equals(ai1, ai2));
    }

    [Fact]
    public async Task List_with_similar_elements_return_one_distinct()
    {
        var projects = new[]
        {
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"}),
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"})
        };

        var ProjectComparer = new ProjectComparer();

        Assert.Equal(1, projects.Distinct(ProjectComparer).ToList().Count);

    }

}