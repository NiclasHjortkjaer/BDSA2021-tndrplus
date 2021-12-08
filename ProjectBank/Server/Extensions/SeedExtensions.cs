using ProjectBank.Infrastructure;

namespace ProjectBank.Server.Extensions;

public static class SeedExtensions
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProjectBankContext>();
            await SeedProjectsAsync(context);
        }
        
        return host;
    }

    private static async Task SeedProjectsAsync(ProjectBankContext context)
    {
        await context.Database.MigrateAsync();
        if (!await context.Projects.AnyAsync())
        {
            //seed some data
            var unknownAccount = new Account("UnknownToken", "Elon", "Musk");
            var aiKeyword = new Keyword("AI");
            var machineLearnKey = new Keyword("Machine Learning");
            var designKey = new Keyword("Design"); 
            var saveListAccount = new Account("AuthorToken", "Bill", "Gates");
            var save2ListAccount = new Account("AuthorTokenNRTWO", "Billy", "Gatorboi");

            var aiProject = new Project("Artificial Intelligence 101")
            { 
                Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey}, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow
            };
            var mlProject = new Project("Machine Learning for dummies")
            {
                Ects = 15, Author = saveListAccount, Description = "Very easy guide just for you", Keywords = new[]{machineLearnKey}, Degree = Degree.PHD, LastUpdated = DateTime.UtcNow
            };
            var designProject = new Project("Design the future")
            {
                Ects = 15, Author = save2ListAccount, Description = "Everything design", Degree = Degree.Master, Keywords = new[]{designKey},
                LastUpdated = DateTime.UtcNow
            };
            context.Projects.AddRange(aiProject, mlProject, designProject);

            await context.SaveChangesAsync();
        }
    }
}