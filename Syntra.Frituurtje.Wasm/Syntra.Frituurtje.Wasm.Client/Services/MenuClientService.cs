using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Syntra.Frituurtje.Wasm.Client.Services
{
    public class MenuClientService : IMenuService
    {
        HttpClient Http { get; init; } = default!;
        public MenuClientService(HttpClient http)
        {
            Http = http;
        }
        public async Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true)
        {

            var result = await Http.GetFromJsonAsync<IEnumerable<MenuTopic>>("api/menu");
            //Because of the way the data is serialized, we need to set the topic for each item
            if(result?.Count() > 0)
            {
                foreach(var topic in result)
                {
                    foreach(var item in topic.MenuItems)
                    {
                        item.Topic = topic;
                    }
                }
            }
            return result ?? [];

        }

        public Task<bool> SetItem(MenuItem selectedItem)
        {
            throw new NotImplementedException();
        }
    }
}
