using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ProjectBank.Client.Scripts;
//Used to support app roles and authorization.
//Taken from: https://code-maze.com/using-app-roles-with-azure-active-directory-and-blazor-webassembly-hosted-apps/

public class CustomAccount : RemoteUserAccount
{
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; } = Array.Empty<string>();
}