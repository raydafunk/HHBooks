using HHbookStore.App.UI.Services.Base;

namespace HHbookStore.App.UI.Services.Authencation
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LogiUserDto loginModel);
    }
}
