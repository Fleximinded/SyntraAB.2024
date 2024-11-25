using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Syntra.EF.Data.Models;
using Syntra.EF.Domain;

namespace Syntra.EF.Web.Components.Pages
{
    public partial class Employees
    {
        [Inject]
        public TimeRegistrationContext TimeRegistrationContext { get; set; } = default!;
        List<Employee>? EmployeeList { get; set; } = null;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                EmployeeList =await TimeRegistrationContext.Employees.ToListAsync();
                StateHasChanged();
            }            
        }
    }
}
