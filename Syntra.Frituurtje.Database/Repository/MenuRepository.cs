using Microsoft.EntityFrameworkCore;
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
    public class MenuRepository : IMenuRepository
    {
        FrituurtjeContext Context { get; set; } = default!;
        public MenuRepository(FrituurtjeContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<MenuTopic>> GetAllTopicsAsync(bool includeItems, bool includeImages)
        {
            var query = Context.Topics.Where(t => t.IsDeleted == false);
            if(includeItems)
            {
                if(includeImages) query = query.Include(s => s.MenuItems).ThenInclude(s => s.Images);
                else query = query.Include(s => s.MenuItems);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync(string topicId)
        {
            return await Context.Items.Where(item => item.Id == topicId).ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsByTopicAsync(string topicName)
        {
            return await Context.Items.Where(item => item.Topic.Title == topicName).ToListAsync();
        }
        public async Task<MenuImage?> GetImagesAsync(string id)
        {
            return await Context.Images.FindAsync(id);
        }
        public async Task<bool> Insert(MenuTopic menuTopic, bool save = true)
        {
            Context.Topics.Add(menuTopic);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Insert(MenuItem menuItem, bool save = true)
        {
            Context.Items.Add(menuItem);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Insert(MenuImage image, bool save = true)
        {
            Context.Images.Add(image);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuTopic menuTopic, bool save = true)
        {
            Context.Topics.Update(menuTopic);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuItem menuItem, bool save = true)
        {
            Context.Items.Update(menuItem);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuImage menuImage, bool save = true)
        {
            Context.Images.Update(menuImage);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuTopic menuTopic, bool save = true)
        {
            Context.Topics.Remove(menuTopic);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuItem menuItem, bool save = true)
        {
            //var item = await Context.Items.FindAsync(menuItem.Id);
            //if(item == null) return false;
            //item.IsDeleted = true;
            //return await Update(item, save);
            Context.Items.Remove(menuItem);
            if(save) return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuImage menuImage, bool save = true)
        {
            Context.Images.Remove(menuImage);
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuTopic menuTopic, bool save = true)
        {
            var existingTopic = await Context.Topics.FindAsync(menuTopic.Id);
            if(existingTopic == null)
            {
                Context.Topics.Add(menuTopic);
            }
            else
            {
                Context.Topics.Update(menuTopic);
            }
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuItem menuItem, bool save = true)
        {
            var existingItem = await Context.Items.FindAsync(menuItem.Id);
            if(existingItem == null)
            {
                Context.Items.Add(menuItem);
            }
            else
            {
                Context.Items.Update(menuItem);
            }
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuImage menuImage, bool save = true)
        {
            var existingImage = await Context.Images.FindAsync(menuImage.Id);
            if(existingImage == null)
            {
                Context.Images.Add(menuImage);
            }
            else
            {
                Context.Images.Update(menuImage);
            }
            if(save)return await Context.SaveChangesAsync() > 0;
            return true;
        }
    }
}
