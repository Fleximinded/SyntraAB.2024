using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.Contracts.Defines;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Db.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Db.Repository
{
    public class MenuRepository : IMenuRepository
    {
        IDbContextFactory<FrituurtjeContext> DbFactory { get; set; } = default!;
        public MenuRepository(IDbContextFactory<FrituurtjeContext> factory)
        {
            DbFactory = factory;
        }
        public async Task<IEnumerable<MenuTopic>> GetAllTopicsAsync(bool includeItems, bool includeImages)
        {
            using var context = DbFactory.CreateDbContext();
            var query = context.Topics.Where(t => t.IsDeleted == false);
            if(includeItems)
            {
                if(includeImages) query = query.Include(s => s.MenuItems).ThenInclude(s => s.Images);
                else query = query.Include(s => s.MenuItems);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync(string topicId)
        {
            using var context = DbFactory.CreateDbContext();
            return await context.Items.Where(item => item.Topic.Id == topicId).ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsByTopicAsync(string topicName)
        {
            using var context = DbFactory.CreateDbContext();
            return await context.Items.Where(item => item.Topic.Title == topicName).ToListAsync();
        }
        public async Task<MenuImage?> GetImagesAsync(string id)
        {
            using var context = DbFactory.CreateDbContext();
            return await context.Images.FindAsync(id);
        }
        public async Task<bool> Insert(MenuTopic menuTopic, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Topics.FindAsync(menuTopic.Id) != null) return false;
            context.Topics.Add(menuTopic);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Insert(MenuItem menuItem, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Items.FindAsync(menuItem.Id) != null) return false;
            context.Items.Add(menuItem);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Insert(MenuImage image, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Images.FindAsync(image.Id) != null) return false;
            context.Images.Add(image);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuTopic menuTopic, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Items.FindAsync(menuTopic.Id) == null) return false;
            context.Topics.Update(menuTopic);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuItem menuItem, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Items.FindAsync(menuItem.Id) == null) return false;
            if(menuItem.Images?.Count > 0)
            {
                await Upsert(menuItem.Images, false);
            }
            context.Items.Update(menuItem);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update(MenuImage menuImage, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            if(await context.Images.FindAsync(menuImage.Id) == null) return false;
            context.Images.Update(menuImage);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuTopic menuTopic, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            context.Topics.Remove(menuTopic);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuItem menuItem, bool save = true)
        {

            using var context = DbFactory.CreateDbContext();
            context.Items.Remove(menuItem);
            if(save) return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete(MenuImage menuImage, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            context.Images.Remove(menuImage);
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuTopic menuTopic, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            var existingTopic = await context.Topics.FindAsync(menuTopic.Id);
            if(existingTopic == null)
            {
                context.Topics.Add(menuTopic);
            }
            else
            {
                context.Topics.Update(menuTopic);
            }
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuItem menuItem, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            var existingItem = await context.Items.Where(t => t.Id==menuItem.Id).AsNoTracking().FirstOrDefaultAsync();
            if(menuItem.Topic != null)
            {
                menuItem.Topic = await context.Topics.FindAsync(menuItem.Topic.Id) ?? menuItem.Topic;
            }
            if(existingItem == null)
            {
                context.Items.Add(menuItem);
            }
            else
            {
                context.Items.Update(menuItem);
            }
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Upsert(MenuImage menuImage, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            var existingImage = await context.Images.FindAsync(menuImage.Id);
            if(existingImage == null)
            {
                context.Images.Add(menuImage);
            }
            else
            {
                context.Images.Update(menuImage);
            }
            if(save)return await context.SaveChangesAsync() > 0;
            return true;
        }
        public async Task<bool> Upsert(IEnumerable<MenuImage> menuImages, bool save = true)
        {
            using var context = DbFactory.CreateDbContext();
            bool ok = true;
            foreach(var image in menuImages)
            {
                if(!await Upsert(image,false)) ok = false;
            }
            if(save) return await context.SaveChangesAsync() > 0?ok:false;
            return ok;
        }
    }
}
