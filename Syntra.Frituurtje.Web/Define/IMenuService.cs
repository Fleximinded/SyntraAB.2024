using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.Web.Define
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuTopic>> GetTopics(bool includeItems=true);
        Task<bool> SetItem(MenuItem selectedItem);
    }
}
