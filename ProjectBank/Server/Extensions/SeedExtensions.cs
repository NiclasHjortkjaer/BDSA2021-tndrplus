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
            var BillyA = new Account("AuthorToken1", "Billy Gates");
            var JytteA = new Account("AuthorToken2", "Jytte Gurdesen");
            var JohnA = new Account("AuthorToken3", "John Lee");

            //Keywords
            var designKey = new Keyword("Design"); 
            var aiKey = new Keyword("AI");
            var machineLearnKey = new Keyword("Machine Learning");
            var environmentKey = new Keyword("Environment");
            var dbKey = new Keyword("Database");
            var secKey = new Keyword("Security");


            
            var sec = new Project("Security")
            { 
                Author = JohnA ,Keywords = new[]{secKey
                }, Degree = Degree.Master,

                Ects = 15, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }

            };
               var secDb = new Project("How to secure data")
            { 
                Author = ElonA ,Keywords = new[]{secKey
                ,dbKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }

            };
             
             
              var secEnvir = new Project("Security and Environment")
            { 
                Author = ElonA ,Keywords = new[]{secKey
                ,environmentKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JytteA
                }

            };
            
            var secMachine = new Project("Security and machinelearning")
            { 
                Author = ElonA ,Keywords = new[]{secKey
                ,machineLearnKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA,JytteA
                }

            };


            var secAi = new Project("Security and AI")
            { 
                Author = ElonA ,Keywords = new[]{secKey
                ,aiKey }, Degree = Degree.Master,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA
                }

            };
            var secDesing = new Project("Website and Security")
            { 
                Author = ElonA ,Keywords = new[]{secKey
                ,designKey }, Degree = Degree.Master,

                Ects = 15, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA
                }

            };

               var DbEnv = new Project("Environment and data")
            { 
                Author = ElonA ,Keywords = new[]{environmentKey,dbKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA
                }

            };
             var DbAi = new Project("Make ai from collected data")
            { 
                Author = ElonA ,Keywords = new[]{aiKey,dbKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA,JohnA,ElonA,BillyA
                }

            };

            var DbMachineProject = new Project("Machine learing from data ")
            { 
                Author = JohnA ,Keywords = new[]{machineLearnKey,dbKey }, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA,JohnA,ElonA}

            };

             var DbDesignProject = new Project("Connect database to website")
            { 
                Author = JohnA ,Keywords = new[]{designKey,dbKey }, Degree = Degree.Master,

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{JytteA,JohnA,ElonA}

            };
           
             var EnvProject = new Project("Crazy Project")
            { 
                Author = JohnA ,Keywords = new[]{designKey, machineLearnKey,environmentKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/defaultproj.jpg",

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
            var designProject = new Project("Create ai robot ")
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
                Author = ElonA ,Keywords = new[]{aiKey, machineLearnKey}, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/ai_watch_illustration_fp_886_500.jpg",

                Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA}

            };
            
            var mlProject = new Project("Machine Learning for dummies")
            {
                Ects = 15, Author = BillyA, Description = "Very easy guide just for you", Keywords = new[]{machineLearnKey}, Degree = Degree.PHD, LastUpdated = DateTime.UtcNow, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/getmedia.ashx.jpeg"
            };
            var designaProject = new Project("Design the future")
            {
                Ects = 15, Author = BillyA, Description = "Everything design", Degree = Degree.Master, Keywords = new[]{designKey}, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/IN_DEsignthinking_Design-Thinking-2.png",
                LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA, BillyA}

            };
            context.Projects.AddRange
                (aiProject, mlProject, designProject, EnvProject, 
                    EnvAIiProject, designaiProject, designaProject, designAi3Project,
                    designAiProject, sec, secAi, secDb,secDesing, secEnvir, secMachine,
                    DbAi, DbEnv, DbDesignProject, DbMachineProject
                );

            await context.SaveChangesAsync();
        }
    }
}
