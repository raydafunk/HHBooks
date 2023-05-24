using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HHbookStore.App.UI.Services.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorge;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public ApiAuthenticationStateProvider(ILocalStorageService localService)
        {
            this._localStorge = localService;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        ///  Getting the Authentication token for login 
        /// </summary>
        /// <returns></returns>
        public  async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await _localStorge.GetItemAsync<string>("accessToken");

            if (savedToken == null)
            {
                return new AuthenticationState(user);
            }

            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            if (tokenContent.ValidTo < DateTime.Now)
            {
                return new AuthenticationState(user);
            }
            var claims  = tokenContent.Claims;

            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }

        /// <summary>
        ///  get the token when logged in to the application
        /// </summary>
        /// <returns></returns>
        public async Task LoggedIn()
        {
            var savedToken = await _localStorge.GetItemAsync<string>("accessToken");
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Jwt"));
            var authState = Task.FromResult( new AuthenticationState(user)); 
            NotifyAuthenticationStateChanged(authState);

        }

        /// <summary>
        ///  get the token when logged in to the application
        /// </summary>
        /// <returns></returns>
        public void LoggedOut()
        {
            var noclaims = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult( new AuthenticationState(noclaims));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
