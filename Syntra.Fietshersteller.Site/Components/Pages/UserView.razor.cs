using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Syntra.Fietshersteller.Db.Models;
using Syntra.Fietshersteller.Site.Services;

namespace Syntra.Fietshersteller.Site.Components.Pages
{
    public partial class UserView
    {
        [Inject]
        UserManager<ApplicationUser> UserManager { get; set; } = default!;
        [Inject]
        AuthenticationStateProvider Auth { get; set; } = default!;
        [Inject]
        IPersonService PersonService { get; set; } = default!;
        ApplicationUser? CurrentUser { get; set; } = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var state = await Auth.GetAuthenticationStateAsync();
            var appUser = await UserManager.GetUserAsync(state.User);
            if(appUser?.PersonId != null)
            {
                appUser.PersonalInfo = await PersonService.GetAsync(appUser.PersonId);
                CurrentUser = appUser;
                StateHasChanged();  
            }
        }
    }
}
