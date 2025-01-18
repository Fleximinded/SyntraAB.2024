using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.Wasm.Shared
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true);
        Task<bool> SetItem(MenuItem selectedItem);
    }
}
