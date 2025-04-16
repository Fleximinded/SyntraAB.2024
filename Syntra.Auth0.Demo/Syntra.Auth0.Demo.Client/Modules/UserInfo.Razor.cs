
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Syntra.Auth0.Demo.Client.Modules
{
    public partial class UserInfo
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationState { get; set; } = default!;

        private ClaimsPrincipal? User { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            User = (await AuthenticationState.GetAuthenticationStateAsync())?.User;
        }
    }
}
