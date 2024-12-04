using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Database.Repository
{
    public class MenuRepositoryTest : IMenuRepository
    {
        public MenuRepositoryTest()
        {

        }
        public async Task<IEnumerable<MenuTopic>> GetAllTopicsAsync(bool includeItems, bool includeImages)
        {
            return new List<MenuTopic> {
                new MenuTopic { Id = "1", Title = "Snacks", Description = "All snacks" },
                new MenuTopic { Id = "2", Title = "Drinks", Description = "All drinks" },
                new MenuTopic { Id = "3", Title = "Desserts", Description = "All desserts" }
            };
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync(string topicId)
        {
            return new List<MenuItem> {
                new MenuItem { Id = "1", Name = "Fries", Price = 2.5m },
                new MenuItem { Id = "2", Name = "Burger", Price = 4.0m },
                new MenuItem { Id = "3", Name = "Chicken Nuggets", Price = 3.0m }
            };
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsByTopicAsync(string topicName)
        {
            return [];
        }
        public async Task<MenuImage?> GetImagesAsync(string id) => null;
        public async Task<bool> Insert(MenuTopic menuTopic, bool save = true) => false;

        public async Task<bool> Insert(MenuItem menuItem, bool save = true) => false;

        public async Task<bool> Insert(MenuImage image, bool save = true) => false;
        public async Task<bool> Update(MenuTopic menuTopic, bool save = true) => false;

        public async Task<bool> Update(MenuItem menuItem, bool save = true) => true;

        public async Task<bool> Update(MenuImage menuImage, bool save = true) => true;

        public async Task<bool> Delete(MenuTopic menuTopic, bool save = true) => true;

        public async Task<bool> Delete(MenuItem menuItem, bool save = true) => false;

        public async Task<bool> Delete(MenuImage menuImage, bool save = true) => false;

        public async Task<bool> Upsert(MenuTopic menuTopic, bool save = true) => false;

        public async Task<bool> Upsert(MenuItem menuItem, bool save = true) => false;

        public async Task<bool> Upsert(MenuImage menuImage, bool save = true) => false;
    }
}
