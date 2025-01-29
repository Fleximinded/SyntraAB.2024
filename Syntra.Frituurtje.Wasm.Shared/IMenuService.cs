using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.Wasm.Shared
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true);
        Task<IEnumerable<MenuItem>> GetTopicItems(string topicId);
        Task<bool> SetItem(MenuItem selectedItem);
    }
}
