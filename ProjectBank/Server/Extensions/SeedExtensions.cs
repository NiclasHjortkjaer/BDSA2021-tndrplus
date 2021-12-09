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
            //Accounts
            var ElonA = new Account("UnknownToken", "Elon Musk");
            var BillyA = new Account("AuthorToken", "Billy Gates");
            var JytteA = new Account("AuthorToken", "Jytte Gurdesen");
            var JohnA = new Account("AuthorToken", "John Lee");

            //Keywords
            var designKey = new Keyword("Design"); 
            var aiKey = new Keyword("AI");
            var machineLearnKey = new Keyword("Machine Learning");
            var environmentKey = new Keyword("Environment");

           
             var EnvProject = new Project("Crazy Project")
            { 
                Author = JohnA ,Keywords = new[]{designKey, machineLearnKey,environmentKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA,JohnA}

            };
            var designaiProject = new Project("Design website with ai")
            { 
                Author = JytteA ,Keywords = new[]{designKey, machineLearnKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA}

            };
             var designAiProject = new Project("Create website with machine learning")
            { 
                Author = JytteA ,Keywords = new[]{designKey, machineLearnKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA}

            };
             var designAi3Project = new Project("Save the worlds animals with ai and machinelearning ")
            { 
                Author = JytteA ,Keywords = new[]{designKey, machineLearnKey,aiKey,environmentKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA}

            };
            //Projects
            var designProject = new Project("create ai robot ")
            { 
                Author = JytteA ,Keywords = new[]{aiKey, environmentKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "We will lookim. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA}

            };
            var EnvAIiProject = new Project("Save Planet with AI")
            { 
                Author = JytteA ,Keywords = new[]{aiKey, environmentKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA}

            };
            var aiProject = new Project("Artificial Intelligence 101")
            { 
                Author = ElonA ,Keywords = new[]{aiKey, machineLearnKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA}

            };
            
            var mlProject = new Project("Machine Learning for dummies")
            {
                Ects = 15, Author = BillyA, Description = "Very easy guide just for you", Keywords = new[]{machineLearnKey}, Degree = Degree.PHD, LastUpdated = DateTime.UtcNow
            };
            var designaProject = new Project("Design the future")
            {
                Ects = 15, Author = BillyA, Description = "Everything design", Degree = Degree.Master, Keywords = new[]{designKey},
                LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA, BillyA}

            };
            context.Projects.AddRange(aiProject, mlProject, designProject);

            await context.SaveChangesAsync();
        }
    }
}