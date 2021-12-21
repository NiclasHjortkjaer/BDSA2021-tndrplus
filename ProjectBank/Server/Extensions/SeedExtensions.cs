namespace ProjectBank.Server.Extensions;

public static class SeedExtensions
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProjectBankContext>();
        await SeedProjectsAsync(context);
        return host;
    }

    private static async Task SeedProjectsAsync(ProjectBankContext context)
    {
        await context.Database.MigrateAsync();
        if (!await context.Projects.AnyAsync())
        {

            //Accounts
            var elonA = new Account("UnknownToken", "Elon Musk");
            var billyA = new Account("AuthorToken1", "Billy Gates");
            var jytteA = new Account("AuthorToken2", "Jytte Gurdesen");
            var johnA = new Account("AuthorToken3", "John Lee");
            var ilseA = new Account("AutherToken4", "Ilse Bruun");
            var stefanA = new Account("AutherToken5","Stefan Guul");
            var jonA = new Account("AutherToken6", "Jon Jonsen");


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
                Author = jonA ,Keywords = new[]{gameKey,designKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game1.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

            var gameAi = new Project("Make a game With AI")
            { 
                Author = jonA ,Keywords = new[]{gameKey,aiKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game2.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

              var gameMa = new Project("Make a game With machine learning")
            { 
                Author = jonA ,Keywords = new[]{gameKey,machineLearnKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game3.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };


            var gameEnvir = new Project("Make a game about the environment")
            { 
                Author = jonA ,Keywords = new[]{gameKey, environmentKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game4.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
            
            var gameDb = new Project("Make a game with a database")
            { 
                Author = jonA ,Keywords = new[]{gameKey, dbKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game5.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
             var gameSec = new Project("Make a game with security")
            { 
                Author = jonA ,Keywords = new[]{gameKey, secKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game1.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
            
            var gameNe = new Project("Make a MMO")
            { 
                Author = jonA ,Keywords = new[]{gameKey, dataComKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game2.jpeg",
                Ects = 15f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

              var gameHard = new Project("Make a Gamestation")
            { 
                Author = jonA ,Keywords = new[]{gameKey, hardKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/game3.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

            var game = new Project("Theory about games")
            { 
                Author = jonA ,Keywords = new[]{gameKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/games.jpg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc",
                LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA,
                }
            };


            var hardDes = new Project("Design and create a  computer")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,designKey
                }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard1.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

            var hardAi = new Project("Make blinker with AI")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,aiKey
                }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/aihardware.jpg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

                var hardMa = new Project("Make blinker with machine learning")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,machineLearnKey
                }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard2.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

                var hardEnv = new Project("Make a trash eating robot ")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,environmentKey
                }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard3.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };

                var hardDb = new Project("Create a server ")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,dbKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard4.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
                 var hardSec = new Project("Create a robot with security")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,secKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard6.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
                 var hardDa = new Project("Create a computer")
            { 
                Author = stefanA ,Keywords = new[]{hardKey,dataComKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard1.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
                var hard = new Project("Create crazy robot")
            { 
                Author = stefanA ,Keywords = new[]{hardKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard2.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{ilseA,jytteA, johnA
                }
            };
             var dataComDes = new Project("Create network service with crisp UI")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey, designKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/datacom2.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };

            var dataComAi = new Project("Create network service with AI")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey, aiKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/datacom3.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };
            
            var dataComMa = new Project("Create network service with machine learning")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey, machineLearnKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/data commmmms-analytics-comms.jpg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };

             var dataComEnvir = new Project("Create network service that can save the environment")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey, environmentKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/datacom4.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };

             var dataComDb = new Project("Create Chatbot with a database")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey, secKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/data-communication.jpg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };
             var dataCom = new Project("Create Chatbot")
            { 
                Author = ilseA ,Keywords = new[]{dataComKey
                }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/datacom5.jpeg",
                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }
            };

            var sec = new Project("Security")
            { 
                Author = johnA ,Keywords = new[]{secKey
                }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/cybersecurity-vs-information-security-illustration.jpg",

                Ects = 15, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }

            };
        
               var secDb = new Project("How to secure data")
            { 
                Author = elonA ,Keywords = new[]{secKey
                ,dbKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/Cybersecurity jpg.jpg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, johnA
                }

            };
             
             
              var secEnvir = new Project("Security and Environment")
            { 
                Author = elonA ,Keywords = new[]{secKey
                ,environmentKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/env2.png",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA, jytteA
                }

            };
            
            var secMachine = new Project("Security and machinelearning")
            { 
                Author = elonA ,Keywords = new[]{secKey
                    ,machineLearnKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/sec1.jpeg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA,jytteA
                }

            };


            var secAi = new Project("Security and AI")
            { 
                Author = elonA ,Keywords = new[]{secKey
                ,aiKey }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/sec2.jpeg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA
                }

            };
            var secDesign = new Project("Website and Security")
            { 
                Author = elonA ,Keywords = new[]{secKey
                ,designKey }, Degree = Degree.Master, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/sec3.jpeg",

                Ects = 15, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA
                }

            };

               var dbEnv = new Project("Environment and data")
            { 
                Author = elonA ,Keywords = new[]{environmentKey,dbKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/db1.jpeg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA,billyA
                }

            };
             var dbAi = new Project("Make ai from collected data")
            { 
                Author = elonA ,Keywords = new[]{aiKey,dbKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/db2.jpeg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA,johnA,elonA,billyA
                }

            };

            var dbMachineProject = new Project("Machine learing from data ")
            { 
                Author = johnA ,Keywords = new[]{machineLearnKey,dbKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/49451dbms1.jpg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA,johnA,elonA}

            };

             var dbDesignProject = new Project("Connect database to website")
            { 
                Author = johnA ,Keywords = new[]{designKey,dbKey }, Degree = Degree.Master, FileUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/Handbook_on_ER-diagram_to_DDL_translations.pdf",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA,johnA,elonA}

            };
           
             var envProject = new Project("Crazy Project")
            { 
                Author = johnA ,Keywords = new[]{designKey, machineLearnKey,environmentKey }, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/defaultproj.jpg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA,johnA}

            };
            var designaiProject = new Project("Design website with ai")
            { 
                Author = jytteA ,Keywords = new[]{designKey, machineLearnKey}, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/des1.jpeg",

                Ects = 7.5f, Description = "im. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA}

            };
             var designAiProject = new Project("Create website with machine learning")
            { 
                Author = jytteA ,Keywords = new[]{designKey, machineLearnKey}, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/des2.jpeg",

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA}

            };
             var designAi3Project = new Project("Save the worlds animals with ai and machinelearning ")
            { 
                Author = jytteA ,Keywords = new[]{designKey, machineLearnKey,aiKey,environmentKey}, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/db2.jpeg",

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA}

            };
            //Projects
            var designProject = new Project("Create ai robot ")
            { 
                Author = jytteA ,Keywords = new[]{aiKey, environmentKey}, Degree = Degree.Bachelor,ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/hard2.jpeg",

                Ects = 7.5f, Description = "We will lookim. Donec vulputate aliquam neque a vulputate. Aliquam sit amet malesuada odio. Pellentesque malesuada felis dapibus lectus maximus ultricies. Duis at auc", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA}

            };
            var envAIiProject = new Project("Save Planet with AI")
            { 
                Author = jytteA ,Keywords = new[]{aiKey, environmentKey}, Degree = Degree.Bachelor,

                Ects = 7.5f, Description = "We will look at how to programme ai that can save the Environemnt", ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/Why-is-Environmental-Awareness-Important.jpg", LastUpdated = DateTime.UtcNow, Accounts = new[]{jytteA}

            };
            var aiProject = new Project("Artificial Intelligence 101")
            { 
                Author = elonA ,Keywords = new[]{aiKey, machineLearnKey}, Degree = Degree.Bachelor, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/ai_watch_illustration_fp_886_500.jpg",

                Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA}

            };
            
            var mlProject = new Project("Machine Learning for dummies")
            {
                Ects = 15, Author = billyA, Description = "Very easy guide just for you", Keywords = new[]{machineLearnKey}, Degree = Degree.Phd, LastUpdated = DateTime.UtcNow, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/getmedia.ashx.jpeg"
            };
            var designaProject = new Project("Design the future")
            {
                Ects = 15, Author = billyA, Description = "Everything design", Degree = Degree.Master, Keywords = new[]{designKey}, ImageUrl = "https://projectbankstorage.blob.core.windows.net/azurecontainer/IN_DEsignthinking_Design-Thinking-2.png",
                LastUpdated = DateTime.UtcNow, Accounts = new[]{elonA, billyA}

            };
            context.Projects.AddRange
                (aiProject, mlProject, designProject, envProject, 
                    envAIiProject, designaiProject, designaProject, designAi3Project,
                    designAiProject, sec, secAi, secDb,secDesign, secEnvir, secMachine,
                    dbAi, dbEnv, dbDesignProject, dbMachineProject,dataCom,
                    dataComDb,dataComEnvir,dataComMa,dataComAi,dataComDes,hard,hardDa,
                    hardSec,hardDb,hardEnv,hardMa,hardAi,hardDes,game,gameHard,gameNe,
                    gameSec,gameDb,gameEnvir,gameMa,gameAi,gameDes
                );

            await context.SaveChangesAsync();
        }
    }
}
