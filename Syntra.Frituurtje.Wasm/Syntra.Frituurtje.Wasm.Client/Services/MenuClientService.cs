using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;

namespace Syntra.Frituurtje.Wasm.Client.Services
{
    public class MenuClientService : IMenuService
    {
        HttpClient Http { get; init; } = default!;
        public MenuClientService(IHttpClientFactory http)
        {
            Http = http.CreateClient("LocalClient");
        }
        public async Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true)
        {

            var result = await Http.GetFromJsonAsync<IEnumerable<MenuTopic>>("api/menu");
            //Because of the way the data is serialized, we need to set the topic for each item
            try { 
            if(result?.Count() > 0)
            {

                foreach(var topic in result)
                {
                    var items = (await Http.GetFromJsonAsync<IEnumerable<MenuItem>>($"api/menu/items/{topic.Id}")) ?? [];
                    topic.MenuItems = items.ToList() ?? [];
                }
            }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //if(result?.Count() > 0)
            //{
            //    foreach(var topic in result)
            //    {
            //        foreach(var item in topic.MenuItems)
            //        {
            //            item.Topic = topic;
            //        }
            //    }
            //}
            return result ?? [];

        }

        public async Task<bool> SetItem(MenuItem selectedItem)
        {
            //var menuJson = JsonSerializer.Serialize(selectedItem,new JsonSerializerOptions() { WriteIndented=true });  
            //var topicJson = JsonSerializer.Serialize(selectedItem.Topic, new JsonSerializerOptions() { WriteIndented = true }); 

            var response = await Http.PutAsJsonAsync("api/menu/item/set", selectedItem);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MenuItem>> GetTopicItems(string topicId)
        {
            return (await Http.GetFromJsonAsync<IEnumerable<MenuItem>>($"api/menu/items/{topicId}")) ?? [];
        }
    }
}
