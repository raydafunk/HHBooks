using Blazored.LocalStorage;
using HHbookStore.App.UI.Services.Base;
using HHbookStore.App.UI.Services.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HHbookStore.App.UI.Services.Authencation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClient = httpClient;
            this._localStorage = localStorage;
            this._authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(LogiUserDto loginModel)
        {
          var response = await _httpClient.LoginAsync(loginModel);

            //store token
            await _localStorage.SetItemAsStringAsync("accessToken", response.Token);

            //change auth state of app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();


            return true;
        }
    }
}
