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
            var IlseA = new Account("AutherToken4", "Ilse Bruun");
            var StefanA = new Account("AutherToken5","Stefan Guul");
            var JonA = new Account("AutherToken6", "Jon Jonsen");


            //Keywords
            var designKey = new Keyword("Design"); 
            var aiKey = new Keyword("AI");
            var machineLearnKey = new Keyword("Machine Learning");
            var environmentKey = new Keyword("Environment");
            var dbKey = new Keyword("Database");
            var secKey = new Keyword("Security");
            var dataComKey = new Keyword("Data Communication and Networks");
            var hardKey  = new Keyword("Hardware");
            var gameKey = new Keyword("Games");
            
            
            

            var gameDes = new Project("Make a game with focus on design")
            { 
                Author = JonA ,Keywords = new[]{gameKey,designKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

            var gameAI = new Project("Make a game With AI")
            { 
                Author = JonA ,Keywords = new[]{gameKey,aiKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

              var gameMa = new Project("Make a game With machine learning")
            { 
                Author = JonA ,Keywords = new[]{gameKey,machineLearnKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };


            var gameEnvir = new Project("Make a game about the environment")
            { 
                Author = JonA ,Keywords = new[]{gameKey, environmentKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
            
            var gameDb = new Project("Make a game with a database")
            { 
                Author = JonA ,Keywords = new[]{gameKey, dbKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
             var gameSec = new Project("Make a game with security")
            { 
                Author = JonA ,Keywords = new[]{gameKey, secKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
            
            var gameNe = new Project("Make a MMO")
            { 
                Author = JonA ,Keywords = new[]{gameKey, dataComKey
                }, Degree = Degree.Bachelor,
                Ects = 15f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

              var gameHard = new Project("Make a Gamestation")
            { 
                Author = JonA ,Keywords = new[]{gameKey, hardKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

            var game = new Project("Theory about games")
            { 
                Author = JonA ,Keywords = new[]{gameKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };


            var hardDes = new Project("Design and create a  computer")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,designKey
                }, Degree = Degree.Master,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

            var hardAi = new Project("Make blinker with AI")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,aiKey
                }, Degree = Degree.Master,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

                var hardMa = new Project("Make blinker with machine learning")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,machineLearnKey
                }, Degree = Degree.Master,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

                var hardEnv = new Project("Make a trash eating robot ")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,environmentKey
                }, Degree = Degree.Master,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };

                var hardDb = new Project("Create a server ")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,dbKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
                 var hardSec = new Project("Create a robot with security")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,secKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
                 var hardDa = new Project("Create a computer")
            { 
                Author = StefanA ,Keywords = new[]{hardKey,dataComKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
                var hard = new Project("Create crazy robot")
            { 
                Author = StefanA ,Keywords = new[]{hardKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{IlseA,JytteA, JohnA
                }
            };
             var dataComDes = new Project("Create network service with crisp UI")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, designKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

            var dataComAI = new Project("Create network service with AI")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, aiKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };
            
            var dataComMa = new Project("Create network service with machine learning")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, machineLearnKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

             var dataComEnvir = new Project("Create network service that can save the environment")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, environmentKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

             var dataComDb = new Project("Create Chatbot with a database")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, secKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

              var dataComSec = new Project("Create Chatbot with security")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey, secKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

             var dataCom = new Project("Create Chatbot")
            { 
                Author = IlseA ,Keywords = new[]{dataComKey
                }, Degree = Degree.Bachelor,
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ElonA,BillyA, JohnA
                }
            };

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
            context.Projects.AddRange
                (aiProject, mlProject, designProject, EnvProject, 
                    EnvAIiProject, designaiProject, designaProject, designAi3Project,
                    designAiProject, sec, secAi, secDb,secDesing, secEnvir, secMachine,
                    DbAi, DbEnv, DbDesignProject, DbMachineProject,dataCom, dataComSec,
                    dataComDb,dataComEnvir,dataComMa,dataComAI,dataComDes,hard,hardDa,
                    hardSec,hardDb,hardEnv,hardMa,hardAi,hardDes,game,gameHard,gameNe,
                    gameSec,gameDb,gameEnvir,gameMa,gameAI,gameDes
                );

            await context.SaveChangesAsync();
        }
    }
}
