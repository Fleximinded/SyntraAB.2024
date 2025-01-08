using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Database.Context;
using Syntra.Frituurtje.Database.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Database.Repository
{
    public class OrderRepository :IOrderRepository
    {
        FrituurtjeContext Context { get; init; } = default!;
        DbSet<FoodOrder> OrderTable { get; init; } = default!;
        public OrderRepository(FrituurtjeContext context) {
            Context = context;
            OrderTable = Context.Orders;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var order = await OrderTable.FindAsync(id);
            if(order != null)
            {
                OrderTable.Remove(order);
                return await Context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<FoodOrder>> GetAllAsync() => await OrderTable.ToArrayAsync();

        public async Task<FoodOrder?> GetByIdAsync(string id) => await OrderTable.FindAsync(id);
        public async Task<bool> AddAsync(FoodOrder order)
        {
            if(await OrderTable.FindAsync(order.Id) == null)
            {
                await OrderTable.AddAsync(order);
                return await Context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(FoodOrder order)
        {
            if(await OrderTable.FindAsync(order.Id) != null)
            {
                OrderTable.Update(order);
                return await Context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpsertAsync(FoodOrder order)
        {
            if(await AddAsync(order) == false) { 
                return await UpdateAsync(order);
            }
            return true;
        }

        public async Task<IEnumerable<FoodOrder>> FindAsync(string? id = null, decimal? price = null, string? client = null, string? itemId = null, DateOnly? orderDate = null)
        {
            var query=OrderTable.AsQueryable();
            if(id != null) { query = query.Where(o => o.Id == id); }    
            if(price != null) { 
                var minPrice= (decimal)price * 0.90m;   
                var maxPrice= (decimal)price * 1.10m;

                query = query.Where(o => o.Price * o.Quantity >= minPrice && o.Price * o.Quantity <= maxPrice);            
            }
            if(client != null)
            {
                query = query.Where(o => o.ClientId.ToLower() == client.ToLower());
            }
            if(itemId != null)
            {
                query = query.Where(o => o.MenuItemId == itemId);
            }
            if(orderDate != null)
            {
                var date=orderDate.Value;
                query = query.Where(o => o.OrderDate.Year == date.Year && o.OrderDate.Month == date.Month && o.OrderDate.Day == date.Day);
            }
            return await query.ToArrayAsync();
        }
    }
}
