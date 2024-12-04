using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Web.Define;

namespace Syntra.Frituurtje.Web.Services
{
    public class MenuService : IMenuService
    {
        IMenuRepository Repository { get; init; }
        public MenuService(IMenuRepository repo) { Repository = repo; }
        public async Task<IEnumerable<MenuTopic>> GetTopics(bool loadItems)
        {
           return await Repository.GetAllTopicsAsync(loadItems,loadItems);
        }

        public async Task<bool> SetItem(MenuItem selectedItem) => await Repository.Upsert(selectedItem);
    }
}
