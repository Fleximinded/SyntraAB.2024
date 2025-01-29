using Microsoft.AspNetCore.Components;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Client.Services;

namespace Syntra.Frituurtje.Wasm.Client.Pages
{
    public partial class OrderOverview
    {
        [Inject] public IMenuOrderService OrderService { get; set; } = default!;
        IEnumerable<FoodOrder>? Orders { get; set; }=null;
        protected override async void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                Orders = await OrderService.GetOrders();
                StateHasChanged();
            }
        }
    }
}
