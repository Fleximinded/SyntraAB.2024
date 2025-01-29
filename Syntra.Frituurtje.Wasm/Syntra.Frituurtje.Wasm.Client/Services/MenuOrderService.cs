using Syntra.Frituurtje.Contracts.Models;
using System.Net.Http.Json;

namespace Syntra.Frituurtje.Wasm.Client.Services
{
    public interface IMenuOrderService
    {
        Task<IEnumerable<FoodOrder>> GetOrders();
    }

    public class MenuOrderService : IMenuOrderService
    {
        HttpClient Http { get; init; } = default!;
        public MenuOrderService(IHttpClientFactory http)
        {
            Http = http.CreateClient("OrderApiClient");
        }

        public async Task<IEnumerable<FoodOrder>> GetOrders()
        {
            var result = await Http.GetFromJsonAsync<IEnumerable<FoodOrder>>("api/orders");
            return result ?? [];
        }
    }
}
