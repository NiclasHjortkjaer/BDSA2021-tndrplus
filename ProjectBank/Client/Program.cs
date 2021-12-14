using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectBank.Client;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ProjectBank.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//builder.Services.AddScoped<NotificationService>(); //is this the right way to do it hmm - its to get the notifications on swipes

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ProjectBank.ServerAPI"));
builder.Services.AddScoped<NotificationService>();

builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomAccount>(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://be2d38af-1a6a-499c-95c5-1235edbfde2f/API.Access");
    options.UserOptions.RoleClaim = "appRole"; 
}).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomAccount,
    CustomAccountFactory>();

await builder.Build().RunAsync();
