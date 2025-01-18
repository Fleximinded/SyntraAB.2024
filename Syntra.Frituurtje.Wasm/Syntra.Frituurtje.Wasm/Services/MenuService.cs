using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm.Services
{
    public class MenuService : IMenuService
    {
        IMenuRepository MenuRepository { get; set; }
        public MenuService(IMenuRepository repo)
        {
            MenuRepository = repo;
        }
        public async Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems = true) {
            return await MenuRepository.GetAllTopicsAsync(true, false);
        }
        public async Task<bool> SetItem(MenuItem selectedItem) { return false; }
       
    }
}
