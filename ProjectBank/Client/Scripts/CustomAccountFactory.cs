using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace ProjectBank.Client.Scripts;

public class CustomAccountFactory : AccountClaimsPrincipalFactory<CustomAccount>
{
    public CustomAccountFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
    {
    }
    
    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(CustomAccount account, 
        RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);
        if (initialUser.Identity != null && initialUser.Identity.IsAuthenticated)
        {
            var userIdentity = (ClaimsIdentity)initialUser.Identity;
            foreach (var role in account.Roles)
            {
                userIdentity.AddClaim(new Claim("appRole", role));
            }
        }
        return initialUser;
    }
    
}