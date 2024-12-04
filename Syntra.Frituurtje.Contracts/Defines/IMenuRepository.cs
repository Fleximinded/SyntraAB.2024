using Syntra.Frituurtje.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Contracts.Defines
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuTopic>> GetAllTopicsAsync(bool includeItems,bool includeImages);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync(string topicId);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsByTopicAsync(string topicName);
        Task<MenuImage?> GetImagesAsync(string id);
        Task<bool> Insert(MenuTopic menuTopic,bool save=true);
        Task<bool> Insert(MenuItem menuItem, bool save = true);
        Task<bool> Insert(MenuImage image, bool save = true);
        Task<bool> Update(MenuTopic menuTopic, bool save = true);
        Task<bool> Update(MenuItem menuItem, bool save = true);
        Task<bool> Update(MenuImage menuImage, bool save = true);
        Task<bool> Delete(MenuTopic menuTopic, bool save = true);
        Task<bool> Delete(MenuItem menuItem, bool save = true);
        Task<bool> Delete(MenuImage menuImage, bool save = true);
        Task<bool> Upsert(MenuTopic menuTopic, bool save = true);
        Task<bool> Upsert(MenuItem menuItem, bool save = true);
        Task<bool> Upsert(MenuImage menuImage, bool save = true);
    }
}
