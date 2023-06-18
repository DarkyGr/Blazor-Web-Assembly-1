using Blazor_WebAssembly_Login_1.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blazor_WebAssembly_Login_1.Client.Extensions
{
    public class AuthenticationExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        
        // Save the information of the user
        private ClaimsPrincipal _withoutInformation = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationExtension(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        // Update the authentication status and save the information of the user
        public async Task UpdateAuthenticationStatus(SessionDTO? sessionUser)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sessionUser != null)
            {
                // Create new Claim declaring its elements
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, sessionUser.Name),
                    new Claim(ClaimTypes.Email, sessionUser.Email),
                    new Claim(ClaimTypes.Role, sessionUser.Rol)
                }, "JwtAuth"));

                // Save the information
                await _sessionStorageService.SaveStorage("sessionUser", sessionUser);
            }
            else
            {
                claimsPrincipal = _withoutInformation;

                await _sessionStorageService.RemoveItemAsync("sessionUser");
            }
            
            // Notify the status to the Authentication service and save the information of the user
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        // Get the information of the user who is authenticated
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Get the session user
            var sessionUser = await _sessionStorageService.GetStorage<SessionDTO>("sessionUser");

            // Check if is null
            if (sessionUser == null)
            {
                return await Task.FromResult(new AuthenticationState(_withoutInformation));
            }

            // If not
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, sessionUser.Name),
                    new Claim(ClaimTypes.Email, sessionUser.Email),
                    new Claim(ClaimTypes.Role, sessionUser.Rol)
                }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
