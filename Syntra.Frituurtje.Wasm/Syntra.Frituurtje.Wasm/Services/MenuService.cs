using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;
using System.Text.Json;

namespace Syntra.Frituurtje.Wasm.Services
{
    public class MenuService : IMenuService
    {
        IMenuRepository MenuRepository { get; init; }
        ILogger<MenuService> Logger { get; init; }   
        public MenuService(IMenuRepository repo,ILogger<MenuService> log)
        {
            MenuRepository = repo;
            Logger = log;
        }
        public async Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true) {
            Logger.LogDebug("Getting topics");
            return await MenuRepository.GetAllTopicsAsync(true, false);
        }
        public async Task<IEnumerable<MenuItem>> GetTopicItems(string topicId)
        {
            Logger.LogDebug($"Getting items for topic '{topicId}'");
            return await MenuRepository.GetAllMenuItemsAsync(topicId);
        }
        public async Task<bool> SetItem(MenuItem selectedItem)
        {
            try
            {
                Logger.LogDebug($"Setting item '{JsonSerializer.Serialize(selectedItem)}'");
                return await MenuRepository.Upsert(selectedItem);
            } catch(Exception ex)
            {
                Logger.LogError(ex, "Error setting item");
                return false;

            }
        }
       
    }
}
