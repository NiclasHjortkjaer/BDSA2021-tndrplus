
namespace Server.Integration.Tests;
//Used for generating and and seeding an HttpClient
//Heavily inspired by Rasmus Lystrøm's BDSA2021 repository on GitHub: https://github.com/ondfisk/BDSA2021/tree/main/MyApp.Server.Integration.Tests
//and https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
public class CustomWebApplicationFactory :  WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProjectBankContext>));

            if (dbContext != null)
            {
                services.Remove(dbContext);
            }

            /* Overriding policies and adding Test Scheme defined in TestAuthHandler */
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Test")
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                    options.DefaultScheme = "Test";
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

            var connection = new SqliteConnection("Filename=:memory:");

            services.AddDbContext<ProjectBankContext>(options =>
            {
                options.UseSqlite(connection);
            });

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<ProjectBankContext>();
            appContext.Database.OpenConnection();
            appContext.Database.EnsureCreated();

            SeedProjects(appContext);
        });

        builder.UseEnvironment("Integration");

        return base.CreateHost(builder);
    }
    
    private void SeedProjects(ProjectBankContext context)
    {
        //seed some data
        var unknownAccount = new Account("UnknownToken", "Elon Musk") {Id = 1};
        var aiKeyword = new Keyword("AI") {Id = 1};
        var machineLearnKey = new Keyword("Machine Learning") {Id = 2};
        var saveListAccount = new Account("AuthorToken", "Billy Gates") {Id = 3};
        var saveListAccount2 = new Account("AuthorToken2", "Billy The Gates") {Id = 4};
        var aiProject = new Project("Artificial Intelligence 101")
        { 
            Id = 1, AuthorId = 1,Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey}, Degree = Degree.Bachelor,
            Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[] {saveListAccount,saveListAccount2}
        };
        var mlProject = new Project("Machine Learning for dummies")
        {
            Id = 2, AuthorId = 1,Author = unknownAccount , Keywords = new[]{aiKeyword, machineLearnKey}, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.Phd, LastUpdated = DateTime.UtcNow
        };
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2", "John Bezos") { Id = 2 });
        context.SaveChanges();

        context.SaveChanges();
    }
}
